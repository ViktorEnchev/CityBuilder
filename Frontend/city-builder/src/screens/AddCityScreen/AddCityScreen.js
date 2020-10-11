import React, { useState } from "react";
import { axiosPost } from "src/axios/axios";
import CenterWrapper from "src/hocs/CenteredWrapper";
import BaseButton from "src/components/BaseButton";
import BaseInput from "src/components/BaseInput";
import AddIcon from "@material-ui/icons/Add";
import NotificationPopup from "src/components/NotificationPopup";
import { makeStyles } from "@material-ui/core";
import NavigationBar from "src/components/NavigationBar";

const useStyles = makeStyles({
  button: {
    marginTop: "10px",
  },
});

const AddCityScreen = (props) => {
  const classes = useStyles();

  const [name, setName] = useState("");
  const [population, setPopulation] = useState(0);

  const [errorMessage, setErrorMessage] = useState("");

  const onAdd = async () => {
    try {
      const response = await axiosPost("city", {
        name,
        population,
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
    return !name || !population;
  };

  return (
    <div>
      <NavigationBar />
      <CenterWrapper fullHeight>
        <BaseInput
          label={"City name"}
          required={true}
          onChange={(e) => setName(e.target.value)}
          placeholder={"e.g. Sofia"}
        />
        <BaseInput
          label={"Population"}
          required={true}
          onChange={(e) => setPopulation(Number(e.target.value))}
          placeholder={"e.g. 241836"}
        />
        <BaseButton
          className={classes.button}
          disabled={isDisabled()}
          text={"Add"}
          onClick={onAdd}
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

export default AddCityScreen;
