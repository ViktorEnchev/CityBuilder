import React from "react";
import PropTypes from "prop-types";
import { makeStyles } from "@material-ui/core";
import cx from "classnames";

const useStyles = makeStyles({
  center: {
    display: "flex",
    justifyContent: "center",
    "align-items": "center",
    "flex-direction": "column",
  },
  fullHeight: {
    height: "100vh",
  },
});

const CenterWrapper = ({ children, fullHeight, className }) => {
  const classes = useStyles();

  return (
    <div
      className={cx(
        classes.center,
        { [classes.fullHeight]: fullHeight },
        className
      )}
    >
      {children}
    </div>
  );
};

CenterWrapper.propTypes = {
  children: PropTypes.node.isRequired,
  fullHeight: PropTypes.bool,
  className: PropTypes.string,
};

export default CenterWrapper;
