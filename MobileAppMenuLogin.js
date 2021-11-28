import React, {Component} from "react";
import {StyleSheet, Text, View, TextInput, TouchableOpacity, StatusBar} from 'react-native';

type Props = {};
export default class App extends Component<Props> {
  render() {
    return (
      <View style={styles.container}>
        <StatusBar
        backgroundColor="#29C5F6"
        barStyle="light-content"
        />
        <Text style={styles.welcome}>Welcome to StreetPatch</Text>
        <TextInput 
        style={styles.input}
        placeholder="Username"
        />
        <TextInput 
        style={styles.input}
        placeholder="Password"
        secureTextEntry
        />
        <View style={styles.btnContainer}>
          <TouchableOpacity
           style={styles.userBtn}
           onPress={() => alert("Login was Succesful")}
          >
            <Text style={styles.btnText}>Login</Text>
          </TouchableOpacity>
          <TouchableOpacity
           style={styles.userBtn}
           onPress={() => alert("Signup was Succesful")}
          >
            <Text style={styles.btnText}>Sign Up</Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#29C5F6'
  },
  welcome: {
    fontSize: 30,
    textAlign: 'center',
    margin: 10,
    color: '#333333'
  },
input: {
  width: "90%",
  backgroundColor: "#fff"
},
btnContainer: {
  flexDirection: "row",
  justifyContent: "space-between",
  width: "90%"
},
 userBtn: {
   backgroundColor: "#F08080",
   padding: 15,
   width: "45%"
},
btnText: {
  fontSize: 18,
  textAlign: "center"
}
});