// Write your JavaScript code.
// Write your JavaScript code.
$(function () {
    if ($('#detailsId').val()) {
        let name = '#modalbtn' + $('#detailsId').val();
        let data = $(name).data('details');
        CopyToModal($('#detailsId').val(), data);
        $('#details_popup').modal('show');
    };
//details click - to load popup on catalogue
    $("a.btn-default").on("click", (e) => {
        let Id = e.target.dataset.id;
        let data = JSON.parse(e.target.dataset.details); // it's a string need an object
        $("#results").text("");
        CopyToModal(Id, data);
    });

    if ($("#register_popup") !== undefined) {
        $('#register_popup').modal('show');
    } 

    if ($("#login_popup") !== undefined) {
        $('#login_popup').modal('show');
    }
  
    

    $('.nav-tabs a').on('show.bs.tab', function (e) {
        if ($(e.relatedTarget).text() === 'Demographic') { // tab 1
            $('#Firstname').valid()
            $('#Lastname').valid()
            if ($('#Firstname').valid() === false || $('#Lastname').valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Address') { // tab 2
            $('#Address1').valid()
            $('#City').valid()
            if ($('#Address1').valid() === false || $('#City').valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Account') { // tab 3 
            $('#Email').valid()
            $('#Password').valid()
            $('#RepeatPassword').valid()
            if ($('#Email').valid() === false || $('#Password').valid() === false || $('#RepeatPassword').valid() === false) {
                return false; // suppress click
            }
        }
    }); // show bootstrap tab
});



let CopyToModal = (id, data) => {
    $("#qty").val("0");
    $("#CostPrice").text(data.CostPrice);
    $("#description").text(data.Description);
    $("#detailsGraphic").attr("src", "/images/" + data.GraphicName);
    $("#detailsId").val(id);
}    // display message if modal still loaded

