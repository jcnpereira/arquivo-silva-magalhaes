/*global google*/

/**
 * Initializes a read-only version
 * of Google Maps.
 */
(function (google) {
    'use strict';

    var $map = $('#map_canvas');

    var lat = parseFloat($map.data('afsm-lat')) || 0;
    var lng = parseFloat($map.data('afsm-lng')) || 0;

    var position = new google.maps.LatLng(lat, lng);

    var mapOptions = {
        center: position,
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(
        document.getElementById('map_canvas'),
        mapOptions);

    new google.maps.Marker({
        position: position,
        map: map,
        draggable: false
    });

    // Center the map on a resize.
    $('a[data-afsm-update-map]').on('shown.bs.tab', function (e) {
        var mapId = $(this).data('afsm-update-map');
        
        google.maps.event.trigger(document.getElementById(mapId), 'resize');
        map.setCenter(position);
    });
}(google));