import React, { useState, useEffect } from "react";
import Select from "@material-ui/core/Select";
import {
  FormControl,
  InputLabel,
  makeStyles,
  MenuItem,
} from "@material-ui/core";
import BaseInput from "src/components/BaseInput";
import BaseButton from "src/components/BaseButton";
import AddIcon from "@material-ui/icons/Add";
import NavigationBar from "src/components/NavigationBar";
import { axiosGet, axiosPost } from "src/axios/axios";
import NotificationPopup from "src/components/NotificationPopup";
import CenterWrapper from "src/hocs/CenteredWrapper";

const useStyles = makeStyles((theme) => ({
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
  row: {
    margin: theme.spacing(1),
    flexDirection: "row",
  },
}));

const AddRoadScreen = (props) => {
  const classes = useStyles();

  const [cities, setCities] = useState([]);

  const [roadName, setRoadName] = useState("");
  const [roadLength, setRoadLength] = useState(0);
  const [firstCityId, setFirstCityId] = useState(0);
  const [secondCityId, setSecondCityId] = useState(0);

  const [errorMessage, setErrorMessage] = useState();

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

  const onAdd = async () => {
    try {
      const response = await axiosPost("road", {
        roadName,
        roadLength,
        firstCityId,
        secondCityId,
      });

      if (response.status === 200) {
        return props.history.replace("/cities");
      }
      setErrorMessage(response.data.message);
    } catch (e) {
      setErrorMessage(e.response.data.message);
    }
  };

  const isDisabled = () => {
    return !(roadName && roadLength && firstCityId && secondCityId);
  };

  const renderSelect = (value, onSelect) => {
    return (
      <Select value={value} onChange={(e) => onSelect(e.target.value)}>
        {cities.map(({ id, name }, index) => (
          <MenuItem value={id}>{name}</MenuItem>
        ))}
      </Select>
    );
  };

  return (
    <div>
      <NavigationBar />
      <CenterWrapper fullHeight>
        <div className={classes.row}>
          <BaseInput
            className={classes.formControl}
            label={"Road name"}
            value={roadName}
            required={true}
            onChange={(e) => setRoadName(e.target.value)}
            placeholder={"e.g. Sofia-Varna"}
          />
          <BaseInput
            className={classes.formControl}
            label={"Road length (km)"}
            value={roadLength}
            required={true}
            onChange={(e) => setRoadLength(Number(e.target.value))}
            placeholder={"e.g. 256"}
          />
        </div>
        <div className={classes.row}>
          <FormControl required className={classes.formControl}>
            <InputLabel>Select first city</InputLabel>
            {renderSelect(firstCityId, setFirstCityId)}
          </FormControl>
          <FormControl required className={classes.formControl}>
            <InputLabel>Select end city</InputLabel>
            {renderSelect(secondCityId, setSecondCityId)}
          </FormControl>
        </div>
        <BaseButton
          className={classes.button}
          disabled={isDisabled()}
          text={"Add"}
          onClick={() => onAdd()}
          endIcon={<AddIcon />}
        />
        <NotificationPopup
          errorMessage={errorMessage}
          setErrorMessage={setErrorMessage}
        />
      </CenterWrapper>
    </div>
  );
};

export default AddRoadScreen;
