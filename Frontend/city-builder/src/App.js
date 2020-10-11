import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import LoginScreen from "./screens/LoginScreen";
import "./App.css";

function App() {
  return (
    <div>
      <BrowserRouter>
        <Switch>
          <Route exact path="/login" component={LoginScreen} />
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
