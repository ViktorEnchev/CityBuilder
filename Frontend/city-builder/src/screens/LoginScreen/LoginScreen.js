import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import CenterWrapper from "src/hocs/CenteredWrapper";
import BaseInput from "src/components/BaseInput";
import BaseButton from "src/components/BaseButton";
import cx from "classnames";
import KeyboardArrowRightIcon from "@material-ui/icons/KeyboardArrowRight";
import { axiosPostUnauth } from "src/axios/axios";
import NotificationPopup from "src/components/NotificationPopup";

const useStyles = makeStyles((theme) => ({
  root: {
    "& .MuiTextField-root": {
      margin: theme.spacing(1),
      width: "25ch",
    },
  },
  loginWrapper: {
    height: "50vh",
    paddingLeft: "70px",
    paddingRight: "70px",
  },
}));

const LoginScreen = (props) => {
  const classes = useStyles();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [errorMessage, setErrorMessage] = useState(null);

  const onLogin = async () => {
    try {
      const response = await axiosPostUnauth("identity", {
        username,
        password,
      });

      if (response.status === 200) {
        localStorage.setItem("token", `Bearer ${response.data.token}`);
        return props.history.replace("/cities");
      }
      setErrorMessage(response.data.message);
    } catch (e) {
      setErrorMessage(e.response.data.message);
    }
  };

  const isDisabled = () => !username || !password;

  return (
    <CenterWrapper fullHeight>
      <div className={cx(classes.center, classes.fullHeight)}>
        <CenterWrapper className={classes.loginWrapper}>
          <form className={classes.root} autoComplete="off">
            <BaseInput
              label={"Username"}
              required={true}
              placeholder={"Username"}
              type={"email"}
              onChange={(e) => setUsername(e.target.value)}
            />
            <BaseInput
              label={"Password"}
              required={true}
              type={"password"}
              onChange={(e) => setPassword(e.target.value)}
            />
            <div>
              <BaseButton
                disabled={isDisabled()}
                text={"Login"}
                onClick={onLogin}
                endIcon={<KeyboardArrowRightIcon />}
              />
            </div>
          </form>
        </CenterWrapper>
      </div>
      <NotificationPopup
        errorMessage={errorMessage}
        setErrorMessage={setErrorMessage}
      />
    </CenterWrapper>
  );
};

export default LoginScreen;
