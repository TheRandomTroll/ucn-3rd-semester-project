import React, {Component} from 'react';
import { Button, View, Image, ImageBackground, StyleSheet, Pressable, TouchableOpacity, Text, } from 'react-native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { NavigationContainer } from '@react-navigation/native';
import { LoginScreen, MapScreen } from './screens'


function StreetPatchScreen ({ navigation }) {
  return (
    <View>

    </View>
  );
}

function CommentsScreen({ navigation }) {
  return (
    <View style={{ flex: 1, alignItems: 'flex-end', justifyContent: 'flex-end' }}>
      <TouchableOpacity
      onPress={() => navigation.goBack()}
          style={{
            width: '100%',
            height: 60,
            backgroundColor: 'white',
            alignItems: 'center',
            justifyContent: 'center',
          }}>
          <Text style={{color: 'black', fontSize: 16}}>Back to the Map</Text>
        </TouchableOpacity>
    </View>
  );
}

function ContactScreen({ navigation }) {
  return (
    <View style={{ flex: 1, alignItems: 'flex-end', justifyContent: 'flex-end' }}>
      <Image style={{position: 'absolute', top: 0}} source={require('../example/assets/streetpatch.png') }  />
      <Text>Contact us pls</Text>
      <TouchableOpacity
      onPress={() => navigation.goBack()}
          style={{
            width: '100%',
            height: 60,
            backgroundColor: 'white',
            alignItems: 'center',
            justifyContent: 'center',
          }}>
          <Text style={{color: 'black', fontSize: 16}}>Back to the Map</Text>
        </TouchableOpacity>
    </View>
  );
}

const Drawer = createDrawerNavigator();


export default function App() {
  return (
    <NavigationContainer>
      <Drawer.Navigator initialRouteName="StreetPatch">
        <Drawer.Screen name="StreetPatch" component={MapScreen} />
        <Drawer.Screen name="Login" component={LoginScreen} />
        <Drawer.Screen name="Comments" component={CommentsScreen} />
        <Drawer.Screen name="Contact" component={ContactScreen} />
      </Drawer.Navigator>
    </NavigationContainer>
  );
}