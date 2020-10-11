import React from "react";
import { BrowserRouter, Redirect, Route, Switch } from "react-router-dom";
import LoginScreen from "./screens/LoginScreen";
import CitiesScreen from "./screens/CitiesScreen";
import AddCityScreen from "./screens/AddCityScreen";
import AddRoadScreen from "./screens/AddRoadScreen";
import CityRoadsScreen from "./screens/CityRoadsScreen";
import "./App.css";

function App() {
  const isAuthenticated = () => localStorage.getItem("token") !== null;
  console.log(localStorage.getItem("token"));
  return (
    <div>
      <BrowserRouter>
        <Switch>
          <Route exact path="/">
            {isAuthenticated() ? (
              <Redirect to="/citites" />
            ) : (
              <Redirect to="/login" component={LoginScreen} />
            )}
          </Route>
          <Route exact path="/login" component={LoginScreen}></Route>
          <Route exact path="/cities" component={CitiesScreen}></Route>
          <Route exact path="/city/add" component={AddCityScreen}></Route>
          <Route exact path="/road/add" component={AddRoadScreen}></Route>
          <Route
            exact
            path="/city-roads/:id"
            component={CityRoadsScreen}
          ></Route>
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
