// import React, { useState, Fragment } from "react";
import ReactDOM from "react-dom";
import React, { useRef, useState } from 'react';
import GoogleMapReact from 'google-map-react';

// const AnyReactComponent = ({ text }) => <div style={{fontSize:18}}>{text}</div>;

const SimpleMap = () => {
  const [mapRef, setMapRef] = useState(null);
  const defaultProps = {
    center: {
      lat: -33.8969759,
      lng: 151.0715249
    },
    zoom: 11
  };

  const handleApiLoaded = (map, maps) => {
    // use map and maps objects
    //     // Store a reference to the google map instance in state
    setMapRef(map);

    const flightPlanCoordinates = [
      { lat: -33.8969759, lng: 151.1715249 },
      { lat: -33.9969759, lng: 151.2715249 },
      { lat: -33.9269759, lng: 151.2715249 },
    ];
    const flightPath = new maps.Polyline({
      path: flightPlanCoordinates,
      geodesic: true,
      strokeColor: "#97e882",
      strokeOpacity: 1.0,
      strokeWeight: 2,
    });
  
    flightPath.setMap(map);

    flightPlanCoordinates.map((cood) => {
      new maps.Marker({
        position: cood,
        map: map,
      });
    })
  };



    return (
      // Important! Always set the container height explicitly
      <div style={{ height: '100vh', width: '100%' }}>
        <GoogleMapReact
          bootstrapURLKeys={{ key: '', libraries:['places, direction'] }}
          defaultCenter={defaultProps.center}
          defaultZoom={defaultProps.zoom}
          yesIWantToUseGoogleMapApiInternals
          onGoogleApiLoaded={({ map, maps }) => handleApiLoaded(map, maps)}
        >
          {/* <AnyReactComponent
            lat={59.955413}
            lng={30.337844}
            text="My Marker"
          /> */}
        </GoogleMapReact>
      </div>
    );
}

export default SimpleMap;
const rootElement = document.getElementById("root");
ReactDOM.render(<SimpleMap />, rootElement);
// // We will use these things from the lib
// // https://react-google-maps-api-docs.netlify.com/
// import {
//   useLoadScript,
//   GoogleMap,
//   Marker,
//   InfoWindow,
//   DirectionsRenderer,
//   DirectionsService,
// } from "@react-google-maps/api";

// const mapContainerStyle = {
//   height: "70vh",
//   width: "100%"
// };

// function App() {
//   // The things we need to track in state
//   const [mapRef, setMapRef] = useState(null);
//   const [selectedPlace, setSelectedPlace] = useState(null);
//   const [markerMap, setMarkerMap] = useState({});
//   const [center, setCenter] = useState({ lat: 44.076613, lng: -98.362239833 });
//   const [zoom, setZoom] = useState(5);
//   const [clickedLatLng, setClickedLatLng] = useState(null);
//   const [infoOpen, setInfoOpen] = useState(false);
//   const [route, setRoute] = useState({});

//   // Load the Google maps scripts
//   const { isLoaded } = useLoadScript({
//     // Enter your own Google Maps API key
//     googleMapsApiKey: "",
//   });

//   // The places I want to create markers for.
//   // This could be a data-driven prop.
//   const myPlaces = [
//     { id: "place1", pos: { lat: 39.09366509575983, lng: -94.58751660204751 } },
//     { id: "place2", pos: { lat: 39.10894664788252, lng: -94.57926449532226 } },
//     { id: "place3", pos: { lat: 39.07602397235644, lng: -94.5184089401211 } }
//   ];

//   // Iterate myPlaces to size, center, and zoom map to contain all markers
//   const fitBounds = map => {
//     const bounds = new window.google.maps.LatLngBounds();
//     myPlaces.map(place => {
//       bounds.extend(place.pos);
//       return place.id;
//     });
//     map.fitBounds(bounds);
//   };

//   const loadHandler = map => {
//     // Store a reference to the google map instance in state
//     setMapRef(map);

//     // const flightPlanCoordinates = [
//     //   { lat: 39.09366509575983, lng: -94.58751660204751 },
//     //   { lat: 39.10894664788252, lng: -94.57926449532226 },
//     //   { lat: 39.07602397235644, lng: -94.5184089401211 },
//     // ];
//     // const flightPath = new window.google.maps.Polyline({
//     //   path: flightPlanCoordinates,
//     //   geodesic: true,
//     //   strokeColor: "#97e882",
//     //   strokeOpacity: 1.0,
//     //   strokeWeight: 2,
//     // });
  
//     // flightPath.setMap(map);
//     // Fit map bounds to contain all markers
//     fitBounds(map);
//   };

//   // We have to create a mapping of our places to actual Marker objects
//   const markerLoadHandler = (marker, place) => {
//     return setMarkerMap(prevState => {
//       return { ...prevState, [place.id]: marker };
//     });
//   };

//   const markerClickHandler = (event, place) => {
//     // Remember which place was clicked
//     setSelectedPlace(place);

//     // Required so clicking a 2nd marker works as expected
//     if (infoOpen) {
//       setInfoOpen(false);
//     }

//     setInfoOpen(true);

//     // If you want to zoom in a little on marker click
//     if (zoom < 13) {
//       // setZoom(13);
//     }

//     // if you want to center the selected Marker
//     setCenter(place.pos)
//   };



//   // const renderMap = () => {
//     return isLoaded && (
//       <Fragment>
//         <GoogleMap
//           // Do stuff on map initial laod
//           onLoad={loadHandler}
//           // Save the current center position in state
//           onCenterChanged={() => setCenter(mapRef.getCenter().toJSON())}
//           // Save the user's map click position
//           onClick={e => setClickedLatLng(e.latLng.toJSON())}
//           center={center}
//           zoom={zoom}
//           mapContainerStyle={mapContainerStyle}
//         >
//           {myPlaces.map(place => (
//             <Marker
//               key={place.id}
//               position={place.pos}
//               onLoad={marker => markerLoadHandler(marker, place)}
//               onClick={event => markerClickHandler(event, place)}

//               // Not required, but if you want a custom icon:
//               // icon={{
//               //   path:
//               //     "M12.75 0l-2.25 2.25 2.25 2.25-5.25 6h-5.25l4.125 4.125-6.375 8.452v0.923h0.923l8.452-6.375 4.125 4.125v-5.25l6-5.25 2.25 2.25 2.25-2.25-11.25-11.25zM10.5 12.75l-1.5-1.5 5.25-5.25 1.5 1.5-5.25 5.25z",
//               //   fillColor: "#0000ff",
//               //   fillOpacity: 1.0,
//               //   strokeWeight: 0,
//               //   scale: 1.25
//               // }}
//             />
//           ))}
//           {/* {infoOpen && selectedPlace && (
//             <InfoWindow
//               anchor={markerMap[selectedPlace.id]}
//               onCloseClick={() => setInfoOpen(false)}
//             >
//               <div>
//                 <h3>{selectedPlace.id}</h3>
//                 <div>This is your info window content</div>
//               </div>
//             </InfoWindow>
//           )} */}
//           {console.log('one')}
//         </GoogleMap>

//         {/* Our center position always in state */}
//         <h3>
//           Center {center.lat}, {center.lng}
//         </h3>

//         {/* Position of the user's map click */}
//         {clickedLatLng && (
//           <h3>
//             You clicked: {clickedLatLng.lat}, {clickedLatLng.lng}
//           </h3>
//         )}

//         {/* Position of the user's map click */}
//         {selectedPlace && <h3>Selected Marker: {selectedPlace.id}</h3>}
//       </Fragment>
//     );
//   // };

//   // return isLoaded ? renderMap() : null;
// }

// const rootElement = document.getElementById("root");
// ReactDOM.render(<App />, rootElement);
