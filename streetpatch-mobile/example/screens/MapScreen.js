import React, { Component } from 'react';
import {
  StyleSheet,
} from 'react-native';
import MapView, { Marker } from 'react-native-maps'
import 'react-native-gesture-handler';
import GetLocation from 'react-native-get-location'
import map_marker from "../assets/map_marker.png";
import { StackNavigator } from 'react-navigation';
import { NavigationContainer } from '@react-navigation/native';



const styles = StyleSheet.create({
  container: {
    ...StyleSheet.absoluteFillObject,
    height: 400,
  },
  map: {
    ...StyleSheet.absoluteFillObject,
  },
});

const LATITUDE_DELTA = 0.01;
const LONGITUDE_DELTA = 0.01;

const initialRegion = {
  latitude: 37.4219983,
  longitude: -122.084,
  latitudeDelta: 0.0922,
  longitudeDelta: 0.0421,
} 


export default class MapScreen extends Component {
  constructor(props) {
    super(props);
  }

  map = null;

  state = {
    region: {
      latitude: 37.4219983,
      longitude: -122.084,
      latitudeDelta: 0.0922,
      longitudeDelta: 0.0421,
    },
    ready: true,
    filteredMarkers: []
  };

  setRegion(region) {
    if(!region) {
      region = initialRegion;
    }
    if (this.state.ready) {
      setTimeout(() => this.map.mapview.animateToRegion(region), 10);
    }
    //this.setState({ region });
  }

  componentDidMount() {
    console.log('Component did mount');
    this.getCurrentPosition();
  }

  getCurrentPosition() {
    try {
      GetLocation.getCurrentPosition({
        enableHighAccuracy: true,
        timeout: 15000,
      })
        .then(location => {
          const region = {
            latitude: location.latitude,
            longitude: location.longitude,
            latitudeDelta: LATITUDE_DELTA,
            longitudeDelta: LONGITUDE_DELTA
          }

          console.log(region);

          this.setRegion(region)
        })
        .catch(error => {
          const { code, message } = error;
          console.warn(code, message);
        })
    } catch (e) {
      alert(e.message || "");
    }
  };

  onMapReady = (e) => {
    if (!this.state.ready) {
      this.setState({ ready: true });
    }
  };

  onRegionChange = (region) => {
    console.log('onRegionChange', region);
  };

  onRegionChangeComplete = (region) => {
    console.log('onRegionChangeComplete', region);
  };


  render() {
    return (
      <MapView
        showsUserLocation
        ref={map => { this.map = map }}
        style={{ ...StyleSheet.absoluteFillObject }}
        initialRegion={initialRegion}
        onMapReady={this.onMapReady}
        showsMyLocationButton={false}
        onRegionChange={this.onRegionChange}
        onRegionChangeComplete={this.onRegionChangeComplete}
        style={StyleSheet.absoluteFill}
        textStyle={{ color: '#bc8b00' }}
        containerStyle={{ backgroundColor: 'white', borderColor: '#BC8B00' }}
        onPress={this.onMapPress}
      >

        <Marker
          coordinate={{ latitude: 37.425725,
            longitude: -122.084 }}
          image={require('../assets/map_marker2.png')}
          title="Test Title"
          description="This is a test"
          onPress={() => this.props.navigation.navigate('Comments')}  
        >
        </Marker >
        
      </MapView>
    );
  }
}