// https://www.codementor.io/@dhananjaykumar/react-for-google-map-api-y25se2dzz

import React, { Component } from 'react';

class Map extends Component {
  constructor(props) {
    super(props);
    this.onScriptLoad = this.onScriptLoad.bind(this)
  }

  onScriptLoad() {
    const map = new window.google.maps.Map(document.getElementById(this.props.id),this.props.options);
    this.props.onMapLoad(map)
  }

  componentDidMount() {
    if (!window.google) {
      const script = document.createElement('script');
      script.type = 'text/javascript';
      script.src = `https://maps.google.com/maps/api/js?key=AIzaSyAyesbQMyKVVbBgKVi2g6VX7mop2z96jBo`;
      script.id = 'googleMaps';
      document.body.appendChild(script);
      script.addEventListener('load', e => {
        this.onScriptLoad()
      })
    } 
    else {
      this.onScriptLoad()
    }
  }

  render() {
    return (
      <div style={{ width: 500, height: 500 }} id={this.props.id} />
    );
  }
}

export default Map;

//

<Map id="myMap" options={{center: { lat: 51.501904, lng: -0.115871 }, zoom: 13}} 	onMapLoad = {this.handleMapLoad}/>  


// App.js

import Map from './Map';
import React from 'react';
import {Component} from 'react';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { 
      map: {},
      traffic : {},
      transit : {},
      bicycling : {}
    }
  }

  handleMapLoad = (map) => {
    this.setState({
      map: map,
      traffic : new window.google.maps.TrafficLayer(),
      transit : new window.google.maps.TransitLayer(),
      bicycling : new window.google.maps.BicyclingLayer(),
    })
  }

  handleLayer = (layer) => {
    console.log(this.state.map);
    switch (layer) {
      case "traffic" : 
        this.state.transit.setMap(null);
        this.state.bicycling.setMap(null);
        this.state.traffic.setMap(this.state.map); 
        break;
      case "transit" :         
        this.state.bicycling.setMap(null);
        this.state.traffic.setMap(null); 
        this.state.transit.setMap(this.state.map);
        break;  
      case "bicycling" : 
        this.state.transit.setMap(null);
        this.state.traffic.setMap(null); 
        this.state.bicycling.setMap(this.state.map);
        break; 
      case "none" : 
        this.state.transit.setMap(null);
        this.state.traffic.setMap(null); 
        this.state.bicycling.setMap(null);  
        break;     
    }          
  }

  render() {
    return (
      <>
        <button onClick = {() => this.handleLayer("transit")}> Transit</button>
        <button onClick = {() => this.handleLayer("traffic")}> Traffic</button>
        <button onClick = {() => this.handleLayer("bicycling")}> Bicycling</button>
        <button onClick = {() => this.handleLayer("none")}> None</button>
        <Map
          id="myMap"
          options={{
            center: { lat: 51.501904, lng: -0.115871 },
            zoom: 13
          }}
          onMapLoad = {this.handleMapLoad}
        />      
       </>
    );    
  }
}

export default App;
