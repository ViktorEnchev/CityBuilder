import React from "react";
import PropTypes from "prop-types";
import MuiAlert from "@material-ui/lab/Alert";
import { Snackbar } from "@material-ui/core";

const NotificationPopup = ({ errorMessage, setErrorMessage }) => {
  const Alert = (props) => {
    return <MuiAlert elevation={4} variant="filled" {...props} />;
  };

  return (
    <Snackbar
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
      open={!!errorMessage}
      autoHideDuration={4000}
      onClose={() => setErrorMessage(null)}
    >
      <Alert onClose={() => setErrorMessage(null)} severity="error">
        {errorMessage}
      </Alert>
    </Snackbar>
  );
};

NotificationPopup.propTypes = {
  errorMessage: PropTypes.string.isRequired,
  setErrorMessage: PropTypes.func.isRequired,
};

export default NotificationPopup;
