/// <binding ProjectOpened='default' />
module.exports = function(grunt) {
  require('jit-grunt')(grunt);

  grunt.initConfig({
    less: {
      development: {
        files: {
            "Content/grayscale.css": "Less/grayscale.less" // destination file and source file
        }
      },
      production: {
        options: {
            compress: true,
            yuicompress: true,
            optimization: 2
        },
        files: {
            "Content/grayscale.min.css": "Less/grayscale.less" // destination file and source file
        }
      }
    },
    watch: {
      styles: {
        files: ['less/**/*.less'], // which files to watch
        tasks: ['less'],
        options: {
          nospawn: true
        }
      }
    }
  });

  grunt.registerTask('default', ['less', 'watch']);
};