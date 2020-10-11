import React from "react";
import { useHistory } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Button,
  Typography,
  makeStyles,
} from "@material-ui/core";

const useStyles = makeStyles({
  typography: {
    marginRight: "100px",
  },
  buttons: {
    flex: 1,
  },
});

const NavigationBar = () => {
  const classes = useStyles();
  const history = useHistory();

  const onLogout = () => {
    localStorage.removeItem("token");
    history.replace("/login");
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography className={classes.typography} variant="h5">
          City Builder
        </Typography>
        <div className={classes.buttons}>
          <Button color="inherit" onClick={() => history.push("/cities")}>
            Cities
          </Button>
          <Button color="inherit" onClick={() => history.push("/city/add")}>
            New City
          </Button>
          <Button color="inherit" onClick={() => history.push("/road/add")}>
            New Road
          </Button>
        </div>
        <Button color="inherit" onClick={() => onLogout()}>
          Logout
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default NavigationBar;
