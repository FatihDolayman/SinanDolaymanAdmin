
function sendComment(moduleId,module) {
    var commenterName = document.getElementById("commenterInput").value; 
    var commentContent = document.getElementById("commentArea").value;

    $.ajax({
        url: '/Comment/SendComment/',
        data: { moduleId, module, commenterName, commentContent },
        type: 'POST',    
        dataType:'JSON',
        success: function () {
            toastr.success('Yorumunuz onaylandıktan sonra yayınlanacaktır.');
            document.getElementById("commentArea").value = "";
        },
        error: function () {            
            alert("hata alındı");
        }

    });
}


function sendCommentReply(commentId,parentCommenterName) {
    var replierName = document.getElementById("replierNameInput" + commentId).value;
    var commentContent = "@" + parentCommenterName + " " + document.getElementById("replyContentArea" + commentId).value;
    
    $.ajax({
        url: '/Comment/SendCommentReply/',
        data: { commentId, commenterName:replierName, commentContent },
        type: 'POST',
        dataType: 'JSON',
        success: function () {
            toastr.success('Yorumunuz onaylandıktan sonra yayınlanacaktır.');
            document.getElementById("replyContentArea" + commentId).value = "";
        },
        error: function () {
            alert("hata alındı");
        }

    });
}

function sendReplyToReply(commentId,replyId,replierName) {
    var commenterName = document.getElementById("childReplierNameInput" + replyId).value;
    var commentContent ="@"+replierName+" "+ document.getElementById("childReplyContentArea" + replyId).value;

    $.ajax({
        url: '/Comment/SendCommentReply/',
        data: { commentId, commenterName, commentContent },
        type: 'POST',
        dataType: 'JSON',
        success: function () {
            toastr.success('Yorumunuz onaylandıktan sonra yayınlanacaktır.');
            document.getElementById("childReplyContentArea" + replyId).value = "";
        },
        error: function () {
            alert("hata alındı");
        }

    });
}