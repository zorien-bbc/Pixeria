function likeDokument(id, count) {
    var doc = document.getElementById("like" + id);
    var info = document.getElementById("count" + id);
    if (doc.innerHTML.includes("empty")) {
        doc.innerHTML = "<span class='glyphicon glyphicon-heart' aria-hidden='true'></span>";
        if (count == 0) {
            info.innerHTML = "Gefällt: " + (count + 1);
        } else {
            info.innerHTML = "Gefällt: " + (count);
        }
    } else {
        doc.innerHTML = "<span class='glyphicon glyphicon-heart-empty' aria-hidden='true'></span>";
        if (count == 0) {
            info.innerHTML = "Gefällt: " + (count);
        } else {
            info.innerHTML = "Gefällt: " + (count - 1);
        }
    }
    $.ajax({
        url: $("#LikeURL").val(),
        type: "POST",
        dataType: "json",
        data: {
            id: id
        },
        error: function (response) {
            if (!response.Success)
                alert("Like Fehlgeschlagen");
        }
    });
}

function subComment(id, e) {
    var textBox = document.getElementById("comment" + id);
    var text = textBox.value;
    if (textBox != '') {
        if (e.keyCode == 13) {
            var doc = document.getElementById("commentDiv" + id);
            doc.innerHTML += "<span>" + $("#UserName").val() + " : " + text + "</span><br />";
            textBox.value = '';
            $.ajax({
                url: $("#CommentURL").val(),
                type: "POST",
                dataType: "json",
                data: {
                    id: id,
                    text: text
                },
                error: function (response) {
                    if (!response.Success)
                        alert("Kommentieren Fehlgeschlagen");
                }
            });
        }
    }
}
function deleteDocument(id) {
    $.ajax({
        url: $("#DeleteLink").val(),
        type: "POST",
        dataType: "json",
        data: {
            id: id
        },
        error: function (response) {
            if (!response.Success)
                alert("Delete Fehlgeschlagen");
        }, success: function (data) {
            location.reload(true);
        }
    });
}
function preview() {
    var file = $("#file")[0].files[0];
    var fileName = file.name;
    var doc = document.getElementById("preview");
    if (isImage(fileName)) {
        doc.innerHTML = "<img id='imgpreview' class='preview'/>";
        $('#imgpreview')[0].src = window.URL.createObjectURL(jQuery("#file").get(0).files[0])
    } else {
        doc.innerHTML = "<video id='videopreview' class='preview' controls></video>";
        $('#videopreview')[0].src = window.URL.createObjectURL(jQuery("#file").get(0).files[0])
    }
}
function getExtension(filename) {
    var parts = filename.split('.');
    return parts[parts.length - 1];
}

function isImage(filename) {
    var ext = getExtension(filename);
    switch (ext.toLowerCase()) {
        case 'jpg':
        case 'gif':
        case 'bmp':
        case 'png':
        case 'jpe':
        case 'svg':
            return true;
    }
    return false;
}
function UploadDokument() {
    var valid = true;
    if (document.getElementById('title').value == null || document.getElementById('title').value.trim() == "") {
        document.getElementById('titleError').innerHTML = "<div class='alert alert-danger' role='alert'>Bitte geben Sie einen Titel ein!</div>";
        valid = false;
    } else {
        document.getElementById('titleError').innerHTML = "";
    }
    if (document.getElementById('file').files.length == 0) {
        document.getElementById('fileError').innerHTML = "<div class='alert alert-danger' role='alert'>Bitte wählen Sie eine Datei aus!</div>";
        valid = false;
    } else {
        document.getElementById('fileError').innerHTML = "";
    }
    if (valid) {
        $.ajax({
            url: $("#UploadURL").val(),
            type: "POST",
            dataType: "json",
            data: function () {
                var data = new FormData();
                data.append("Titel", jQuery("#title").val());
                data.append("file", jQuery("#file").get(0).files[0]);
                return data;
            }(), contentType: false,
            processData: false,
            error: function (response) {
                if (!response.Success)
                    alert("Upload Fehlgeschlagen");
            }, success: function (data) {
                window.location.replace($("#HomeURL").val());
            }
        });
    }
}
function switchModal(id,e) {
    if (e.keyCode == 37) {
        var target = $('#linkleft' + id).attr("data-target");
        if (target != null) {
            $(target).modal('show');
            $('#myModal'+id).modal('hide');
        }
    } else if (e.keyCode == 39) {
        var target = $('#linkright' + id).attr("data-target");
        if (target != null) {
            $(target).modal('show');
            $('#myModal' + id).modal('hide');
        }
    }
}