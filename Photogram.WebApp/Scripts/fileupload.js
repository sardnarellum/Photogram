function uploadFile(file, url) {
    var xhr = new XMLHttpRequest();
    var fd = new FormData();

    xhr.open("POST", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log(xhr.responseText);
        }
    }

    fd.append("upload_file", file);
    xhr.send(fd);
}

var uploadfiles = document.querySelector('#fileupload');

uploadfiles.addEventListener('change', function () {
    var files = this.files;
    for (var i = 0; i < files.length; ++i) {
        uploadFile(this.files[i], 'Media/AjaxUpload');
    }
}, false);