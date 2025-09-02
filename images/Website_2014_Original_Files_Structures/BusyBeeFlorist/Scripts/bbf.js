/*************************/
/*      CATEGORY PAGE   */
/*************************/
function getHash() {
    //try to load the flower item if a user comes from a link with a hash (ie: #15)
    if (window.location.hash) {
        // Fragment exists               
        var type = window.location.hash.substr(1);
        if (!isNaN(type)) {
            fetchItem(type)
        }
    }

}

function fetchItem(id) {

    var $container = $('#flower-item');
    $("#loading").show()

    $.ajax({
        type: "GET",
        url: "/fetchItem.ashx?anticache=" + Math.random(),
        data: { id: id },
        async: true,
        success: function (data) {
            //alert(data)
            //document.getElementById("flower-item").innerHTML = data;

            $container.hide().html(data).fadeIn()
            $("#loading").fadeOut()
            //$("#loading").fadeOut(function () {
            // without this the DOM will contain multiple elements
            // with the same ID, which is bad.
            //$(this).remove();

            //$container.hide().html(data).fadeIn();
            //});
        },
        error: function (data) {
            alert("Error loading the selected flower item (" + id + ")")
        }
    });


}

/*************************/
/*      ORDER PAGE        */
/*************************/

function fill() {

    $("input[type=text].form-control").each(function (index, el) {
        el.value = el.name;
        if (el.name == "sEmail" || el.name == "rEmail") el.value = "kyle@instil.org.au"
        if (el.name == "pCardNumber") el.value = "4111 1111 1111 1111"
    });

    $("input[type=number].form-control").each(function (index, el) {
        el.value = el.name;
        if (el.name == "pCardExpiry") el.value = "1115"
    });

}