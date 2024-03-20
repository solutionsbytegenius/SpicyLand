//$(function (){
//    var PlaceHolderElement = $('#PlaceHolderHere');
//    $('button[data-toggle="ajax-modal"]').click(function (event) {
//        var url = $(this).data('url');
//        var decodeUrl = decodeURIComponent(url);
//        $.get(decodeUrl).done(function (data) {
//            PlaceHolderElement.html(data);
//            PlaceHolderElement.find('.modal').modal('show');

//        })
//    })


//    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {

//        var form = $(this).parents.('.modal').find('form');
//        var actionUrl = form.attr('action');
//        console.log("actionURL = ", actionUrl);
//        var url = "/panini/" + actionUrl;
//        console.log("URL = ", url);
//        var sendData = form.serialize();
//        $.post(url, sendData).done(function (data) {
//            PlaceHolderElement.find('.modal').modal('hide');
//        })
//    })
//})