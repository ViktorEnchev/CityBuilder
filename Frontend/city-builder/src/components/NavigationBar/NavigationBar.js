import React from "react";
import { useHistory } from "react-router-dom";
import { AppBar, Toolbar, Button, Typography } from "@material-ui/core";

const NavigationBar = () => {
  const history = useHistory();

  const onLogout = () => {
    localStorage.removeItem("token");
    history.replace("/login");
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Button color="inherit" onClick={() => history.push("/cities")}>
          Cities
        </Button>
        <Button color="inherit" onClick={() => history.push("/city/add")}>
          New City
        </Button>
        <Button color="inherit" onClick={() => history.push("/road/add")}>
          New Road
        </Button>
        <Button color="inherit" onClick={() => onLogout()}>
          Logout
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default NavigationBar;
