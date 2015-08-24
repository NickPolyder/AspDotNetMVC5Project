
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
$("#Categories").addClass("form-control").attr("data-dd", "CheckIt");

$("#Categories[data-dd=CheckIt").change(function () {
    var $put = $(this);
    //$("#TargetArea").load("/Products/getProductsBy/" + $put.val(), function (status) {
    //    if(status == "error" || status == "parsererror")
    //    {
    //        alert("Something happened");
    //    }
      
    //});
    $.ajax({
        url: "/Products/getProductsBy/" + $put.val(),
        data: null,
        beforeSend: function () {
            $("#TargetArea").empty().append('<img src="/Content/img/loading_spinner.gif" alt="Loading" style="margin: 0 auto; padding:0 auto;" />');
        },
        complete: function () {
           // $("#TargetArea").empty().append("Ready!!");
        },
        error: function () {
            $("#TargetArea").empty().append("Sorry we had a problem!");
        },
        success: function (data) {

            $("#TargetArea").empty().append(data);
        }
    });
});



$("#ProdCategories").addClass("form-control");
$("#PDFCategories").addClass("form-control");

$("[data-disabled=disabled]").css("display", "none");