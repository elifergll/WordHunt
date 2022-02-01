$(document).ready(function () {
    $(".question-container").on("click", ".iamok", function () {
        var data = JSON.stringify(
            "Question" = $().val(),
            "Option" = $().val()
        );
    });
});