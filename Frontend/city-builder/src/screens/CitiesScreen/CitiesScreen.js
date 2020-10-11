import React, { useEffect, useState } from "react";
import { axiosGet } from "src/axios/axios";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import KeyboardArrowRightIcon from "@material-ui/icons/KeyboardArrowRight";
import NotificationPopup from "src/components/NotificationPopup";
import NavigationBar from "src/components/NavigationBar";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import CenterWrapper from "src/hocs/CenteredWrapper";
import BaseButton from "src/components/BaseButton";
import BaseInput from "src/components/BaseInput";

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
  row: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    marginTop: "10px",
  },
});

const CitiesScreen = (props) => {
  const classes = useStyles();

  const [cities, setCities] = useState([]);
  const [errorMessage, setErrorMessage] = useState("");
  const [cityId, setCityId] = useState();

  useEffect(() => {
    (async () => {
      try {
        const response = await axiosGet("city/all");

        console.log(response);
        if (response.status === 200) {
          return setCities(response.data.cities);
        }
        setErrorMessage(response.data.errorMessage);
      } catch (e) {
        setErrorMessage(e.response.data.errorMessage);
      }
    })();
  }, []);

  const isDisabled = () => !cityId;

  return (
    <CenterWrapper>
      <NavigationBar />
      {/* <List>
        {cities.map(({ id, name }, index) => (
          <ListItem key={index} button component="a">
            <ListItemText primary={name} />
          </ListItem>
        ))}
      </List> */}
      {cities && (
        <TableContainer component={Paper}>
          <Table className={classes.table} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell align="center">Id</TableCell>
                <TableCell align="center">City Name</TableCell>
                <TableCell align="center">Population</TableCell>
                <TableCell align="center">Created</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {cities.map((city) => (
                <TableRow key={city.id} href={`/city-roads/${city.id}`}>
                  <TableCell align="center">{city.id}</TableCell>
                  <TableCell align="center">{city.name}</TableCell>
                  <TableCell align="center">{city.population}</TableCell>
                  <TableCell align="center">{city.cityCreatedTime}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
      <div className={classes.row}>
        <BaseInput
          label={"City Id"}
          required={true}
          placeholder={"e.g. 1"}
          type={"email"}
          onChange={(e) => setCityId(e.target.value)}
        />
        <BaseButton
          disabled={isDisabled()}
          text={"Roads"}
          onClick={() => {}}
          endIcon={<KeyboardArrowRightIcon />}
        />
      </div>
      <NotificationPopup
        errorMessage={errorMessage}
        setErrorMessage={setErrorMessage}
      />
    </CenterWrapper>
  );
};

export default CitiesScreen;
