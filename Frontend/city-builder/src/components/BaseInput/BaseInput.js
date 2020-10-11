import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";

const BaseInput = ({
  className,
  type,
  label,
  required,
  placeholder,
  onChange,
}) => {
  return (
    <TextField
      required={required}
      label={label}
      placeholder={placeholder}
      type={type}
      onChange={onChange}
      className={className}
    />
  );
};

BaseInput.propTypes = {
  text: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  required: PropTypes.bool.isRequired,
  onChange: PropTypes.func.isRequired,
  placeholder: PropTypes.string,
};

export default BaseInput;
