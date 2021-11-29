import React, {Component} from 'react';
import { Button, View } from 'react-native';
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
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Button onPress={() => navigation.goBack()} title="Back to the map" />
    </View>
  );
}

function ContactScreen({ navigation }) {
  return (
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Button onPress={() => navigation.goBack()} title="Back to the Map" />
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