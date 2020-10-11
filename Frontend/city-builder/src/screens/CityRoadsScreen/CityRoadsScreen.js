import React, { useEffect, useState } from "react";
import {
  IconButton,
  makeStyles,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import { axiosDelete, axiosGet } from "src/axios/axios";
import CenterWrapper from "src/hocs/CenteredWrapper";
import NavigationBar from "src/components/NavigationBar";
import DialogModal from "src/components/DialogModal";
import NotificationPopup from "src/components/NotificationPopup";

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

const CityRoadsScreen = (props) => {
  const classes = useStyles();

  const [city, setCity] = useState({
    id: 0,
    name: null,
    population: 0,
    cityCreatedTime: null,
    roads: [],
  });
  const [modalOpen, setModalOpen] = useState(false);
  const [roadId, setRoadId] = useState();
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const { id } = props.match.params;
    (async () => {
      try {
        const response = await axiosGet(`city/${id}`);

        if (response.status === 200) {
          const {
            id,
            name,
            population,
            cityCreatedTime,
            roads,
          } = response.data;
          return setCity({ id, name, population, cityCreatedTime, roads });
        }
        setErrorMessage(response.data.errorMessage);
      } catch (e) {
        setErrorMessage(e.response.data.errorMessage);
      }
    })();
  }, []);

  const onRoadDelete = async () => {
    try {
      const response = await axiosDelete(`road/${roadId}`);

      if (response.status === 200) {
        return setCity({
          ...city,
          roads: city.roads.filter((r) => r.id !== roadId),
        });
      }
      setErrorMessage(response.data.errorMessage);
    } catch (e) {
      setErrorMessage(e.response.data.errorMessage);
    }
  };

  return (
    <CenterWrapper>
      <NavigationBar />
      {city && (
        <TableContainer component={Paper}>
          <Table className={classes.table} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell align="center">Id</TableCell>
                <TableCell align="center">Road Name</TableCell>
                <TableCell align="center">Road Length (km)</TableCell>
                <TableCell align="center">Start City</TableCell>
                <TableCell align="center">End City</TableCell>
                <TableCell align="center">Created</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {city.roads.map((road) => (
                <TableRow key={road.id}>
                  <TableCell align="center">{road.id}</TableCell>
                  <TableCell align="center">{road.roadName}</TableCell>
                  <TableCell align="center">{road.roadLength}</TableCell>
                  <TableCell align="center">{city.name}</TableCell>
                  <TableCell align="center">{road.secondCity.name}</TableCell>
                  <TableCell align="center">{road.roadCreatedTime}</TableCell>
                  <IconButton
                    edge="end"
                    onClick={() => {
                      setModalOpen(true);
                      setRoadId(road.id);
                    }}
                  >
                    <DeleteIcon />
                  </IconButton>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
      <NotificationPopup
        errorMessage={errorMessage}
        setErrorMessage={setErrorMessage}
      />
      <DialogModal
        onConfirm={onRoadDelete}
        modalOpen={modalOpen}
        setModalOpen={setModalOpen}
      />
    </CenterWrapper>
  );
};

export default CityRoadsScreen;
