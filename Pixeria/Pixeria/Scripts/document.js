function likeDokument(id,count) {
    var doc = document.getElementById("like" + id);
    var info = document.getElementById("count" + id);
    if(doc.innerHTML.includes("empty")){
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
                    text:text
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