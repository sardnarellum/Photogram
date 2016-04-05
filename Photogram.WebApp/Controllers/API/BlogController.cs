using Photogram.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Photogram.WebApp.Controllers.API
{ 
    [Authorize]
    public class BlogController : BaseApiController
    {
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            var posts = _db.BlogPost.AsEnumerable().Select(
                x => new
                {
                    Id = x.Id,
                    Modified = x.Modified.ToJavascriptTimestamp(),
                    Title = x.Title,
                    Lead = x.Lead,
                    Visible = x.Visible,
                    Published = x.Published?.ToJavascriptTimestamp()
                });

            if (!User.Identity.IsAuthenticated)
            {
                var visiblePosts = posts.Where(x => x.Visible).Select(
                    x => new
                    {
                        Id = x.Id,
                        Modified = x.Modified,
                        Title = x.Title,
                        Lead = x.Lead,
                        Published = x.Published
                    });

                return Ok(visiblePosts);
            }

            return Ok(posts);
        }   
        
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var post = await _db.BlogPost.Include("Tags")
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var isAuthed = User.Identity.IsAuthenticated;

            if (null == post
                || (!post.Visible && !isAuthed))
            {
                return NotFound();
            }

            if (!isAuthed)
            {
                var dto = new
                {
                    Id = post.Id,
                    Modified = post.Modified.ToJavascriptTimestamp(),
                    Title = post.Title,
                    Lead = post.Lead,
                    Body = post.Body,
                    Published = post.Published,
                    Tags = post.Tags.Select(x => new
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                };

                return Ok(dto);
            }

            var data = new
            {
                Id = post.Id,
                Modified = post.Modified.ToJavascriptTimestamp(),
                Title = post.Title,
                Lead = post.Lead,
                Body = post.Body,
                Visible = post.Visible,
                Published = post.Published,
                Tags = post.Tags.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };

            return Ok(data);
        }     

        public async Task<IHttpActionResult> Post(BlogPostDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new BlogPost
            {
                Title = data.Title,
                Modified = DateTime.UtcNow,
                Visible = false
            };

            _db.BlogPost.Add(post);
            await _db.SaveChangesAsync();

            var response = new
            {
                Id = post.Id,
                Modified = post.Modified.ToJavascriptTimestamp(),
                Title = post.Title,
                Lead = post.Lead,
                Visible = post.Visible
            };

            return Ok(response);
        }

        public async Task<IHttpActionResult> PutAsync(int id, BlogPostDTO dto)
        {
            var updatable = await _db.BlogPost.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (null == updatable)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modified = false;

            if (modified = !string.Equals(updatable.Title, dto.Title))
            {
                updatable.Title = dto.Title;
            }
            

            if (modified = null != dto.Lead)
            {
                updatable.Lead = dto.Lead;
            }

            if (modified = null != dto.Body)
            {
                updatable.Body = dto.Body;
            }

            if (modified = null != dto.Tags)
            {
                updatable.Tags = dto.Tags;
            }

            if (updatable.Visible = dto.Visible)
            {
                if (null == updatable.Published)
                {
                    updatable.Published = DateTime.UtcNow;
                }
            }
            else
            {
                updatable.Published = null;
            }

            if (modified)
            {
                updatable.Modified = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();

            var response = new
            {
                Id = updatable.Id,
                Modified = updatable.Modified.ToJavascriptTimestamp(),
                Published = updatable.Published?.ToJavascriptTimestamp(),
                Body = updatable?.Body,
                Title = updatable.Title,
                Lead = updatable?.Lead,
                Visible = updatable.Visible
            };

            return Ok(response);
        }

        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var deletable = await _db.BlogPost.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (null == deletable)
            {
                return NotFound();
            }

            _db.BlogPost.Remove(deletable);

            await _db.SaveChangesAsync();

            return Ok(true);
        }
    }
}
