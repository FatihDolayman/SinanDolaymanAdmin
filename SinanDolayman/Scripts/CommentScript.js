
function sendComment(moduleId,module) {
    let commenterName = document.getElementById("commenterInput").value; 
    let commentContent = document.getElementById("commentArea").value;

    $.ajax({
        url: '/Comment/SendComment/',
        data: { moduleId, module, commenterName, commentContent },
        type: 'POST',    
        dataType:'JSON',
        success: function (success) {
            if (success===true) {
                toastr.success('Yorumunuz onaylandıktan sonra yayınlanacaktır.');
                document.getElementById("commentArea").value = "";
            }
            else {
                toastr.error('Yorum en az 2 karakter olmalıdır.');
                document.getElementById("commentArea").value = "";
            }
        },
        error: function () {            
            alert("hata alındı");
        }

    });
}


function sendCommentReply(commentId,parentCommenterName) {
    let replierName = document.getElementById("replierNameInput" + commentId).value;
    let commentContent = "@" + parentCommenterName + " " + document.getElementById("replyContentArea" + commentId).value;
    
    $.ajax({
        url: '/Comment/SendCommentReply/',
        data: { commentId, commenterName:replierName, commentContent },
        type: 'POST',
        dataType: 'JSON',
        success: function (success) {
            if (success===true) {
                toastr.success('Yorumunuz onaylandıktan sonra yayınlanacaktır.');
                document.getElementById("replyContentArea" + commentId).value = "";
            }
            else {
                toastr.error('Yorumunuz en az 2 karakter olmalıdır.');
                document.getElementById("replyContentArea" + commentId).value = "";
            }
        },
        error: function () {
            alert("hata alındı");
        }

    });
}

function sendReplyToReply(commentId,replyId,replierName) {
    let commenterName = document.getElementById("childReplierNameInput" + replyId).value;
    let commentContent ="@"+replierName+" "+ document.getElementById("childReplyContentArea" + replyId).value;

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