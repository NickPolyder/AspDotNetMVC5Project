
$("a[data-name=DelPhoto]").click(function (event) {
    var $input = $(this);
        event.preventDefault();
        if(confirm("Are you sure you want to delete this photo?"))
        {
            var input = $input.attr("data-id");
            $("#UploadPhotoDiv").load('/Products/CreateUploadForm');
            $("#results").css("display", "block");
            $('#results').load('/Products/DeletePhoto/' + input);

        }
});

$("a[data-name=DelPDF]").click(function (event) {
    var $input = $(this);
    event.preventDefault();
    if (confirm("Are you sure you want to delete this PDF?")) {
        var input = $input.attr("data-id");
        $("#UploadPhotoDiv").load('/PdfFiles/CreateUploadForm');
        $("#results").css("display", "block");
        $('#results').load('/PdfFiles/DeletePDF/' + input);

    }
});

$("#sidemenu li").click(function () {
    var $li = $(this);
    $("#sidemenu li").removeClass("active");
    $li.addClass("active");

});
$("a[data-name=ForUs]").click(function (event) {
    var $input = $(this);
    event.preventDefault();
    
    $("#Target_Results").load("/Home/AboutUsPartial/");
   
});

$("a[data-name=Checked]").click(function (event) {
    var $input = $(this);
    event.preventDefault();
    
    $("#Target_Results").load("/PdfFiles/getPDFbyCategory/"+$input.attr("data-id"));

});



$("img[data-photo=true]").width("30%").css("float", "right");

$("#ProdCategories").addClass("form-control");
$("#PDFCategories").addClass("form-control");

$("[data-disabled=disabled]").css("display", "none");