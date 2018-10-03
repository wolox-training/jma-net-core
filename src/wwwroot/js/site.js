// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#addCommentButton").click(function () { 
        var text = $('#commentTextArea').val();
        var movieId = $('#movieID').val();
        $.ajax({
            type: "POST",  
            url: "../../api/v1/CommentAPI/AddComment",
            data: { "id": movieId, "commentText": text },
            failure: function (data) {  
                alert("Could not add comment.");  
            },
            error: function (data) {  
                alert("Error adding comment.");  
            },
            complete: function (jqXHR, state) {
                $('.table').append( `<tr><td>${text}</td></tr>`);
            }
        });         
    });
});
