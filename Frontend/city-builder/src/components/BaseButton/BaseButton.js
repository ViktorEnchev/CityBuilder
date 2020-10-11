import React from "react";
import PropTypes from "prop-types";
import Button from "@material-ui/core/Button";

const BaseButton = ({ className, text, disabled, endIcon, onClick }) => {
  return (
    <Button
      variant={"contained"}
      color={"primary"}
      disabled={disabled}
      onClick={onClick}
      endIcon={endIcon}
      className={className}
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
  className: PropTypes.string,
};

export default BaseButton;
