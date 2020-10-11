import React from "react";
import PropTypes from "prop-types";
import Button from "@material-ui/core/Button";

const BaseButton = ({ text, disabled, endIcon, onClick }) => {
  return (
    <Button
      variant={"contained"}
      color={"primary"}
      disabled={disabled}
      onClick={onClick}
      endIcon={endIcon}
    >
      {text}
    </Button>
  );
};

BaseButton.propTypes = {
  text: PropTypes.string.isRequired,
  disabled: PropTypes.bool.isRequired,
  onClick: PropTypes.func.isRequired,
  endIcon: PropTypes.node,
};

export default BaseButton;
