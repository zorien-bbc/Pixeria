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
    var textBox = document.getElementById("comment" + id).value;
    if (textBox != '') {
        if (e.keyCode == 13) {
            var doc = document.getElementById("commentDiv" + id);
            doc.append("<span>@comment.User.Username :"+ textBox +"</span><br />");
            //$.ajax({
            //    url: $("#CommentURL").val(),
            //    type: "POST",
            //    dataType: "json",
            //    data: {
            //        id: id,
            //        text:textBox
            //    },
            //    error: function (response) {
            //        if (!response.Success)
            //            alert("Kommentieren Fehlgeschlagen");
            //    }
            //});
        }
    }
}