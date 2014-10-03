/*global google*/

/**
 * Initializes a read-only version
 * of Google Maps.
 */
(function (google) {
    'use strict';

    var coords = $('#map_canvas').data('afsm-coords').split(',');

    var lat = parseFloat(coords[0]);
    var lng = parseFloat(coords[1]);

    var position = new google.maps.LatLng(lat, lng);

    var mapOptions = {
        center: position,
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(
        document.getElementById("map_canvas"),
        mapOptions);

    new google.maps.Marker({
        position: position,
        map: map,
        draggable: false
    });
}(google));