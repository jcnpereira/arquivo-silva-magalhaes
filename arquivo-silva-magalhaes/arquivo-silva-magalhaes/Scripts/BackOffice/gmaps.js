(function initialize() {
    var mapContainer = document.getElementById('map_canvas');

    var inputElement = $(mapContainer.getAttribute('data-afsm-for'));

    var lat = 39.603539,
        lng = -8.415085;

    if (inputElement.val()) {
        var coords = inputElement.val().split(',');

        lat = parseFloat(coords[0]);
        lng = parseFloat(coords[1]);
    }

    var position = new google.maps.LatLng(lat, lng);

    var mapOptions = {
        center: position,
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    inputElement.val(position.lat() + "," + position.lng());

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

        inputElement.val(newPos.lat() + "," + newPos.lng());
    });
}());