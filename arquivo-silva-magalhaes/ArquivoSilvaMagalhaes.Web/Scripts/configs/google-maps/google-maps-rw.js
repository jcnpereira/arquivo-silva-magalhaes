/*global google*/
(function initialize() {
    var mapContainer = document.getElementById('map_canvas');
    var $map = $(mapContainer);

    //var inputElement = $(mapContainer.getAttribute('data-afsm-for'));
    var $lat = $($map.data('afsm-lat'));
    var $lng = $($map.data('afsm-lng'));

    var lat = 39.603539,
        lng = -8.415085;

    if ($lat.val() !== '') {
        lat = parseFloat($lat.val());
    }

    if ($lng.val() !== '') {
        lng = parseFloat($lng.val());
    }

    var position = new google.maps.LatLng(lat, lng);

    var mapOptions = {
        center: position,
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    $lat.val(position.lat());
    $lng.val(position.lng());

    var map = new google.maps.Map(
        document.getElementById("map_canvas"),
        mapOptions);

    var marker = new google.maps.Marker({
        position: position,
        map: map,
        draggable: true
    });

    // Update the position on marker drop.
    google.maps.event.addListener(marker, 'dragend', function () {
        var newPos = marker.getPosition();

        $lat.val(newPos.lat());
        $lng.val(newPos.lng());
    });
}());