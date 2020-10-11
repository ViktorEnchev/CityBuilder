import React from "react";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@material-ui/core";
import BaseButton from "../BaseButton";

const DialogModal = ({ onConfirm, modalOpen, setModalOpen }) => {
  return (
    <Dialog open={modalOpen} onClose={() => setModalOpen(false)}>
      <DialogTitle id="alert-dialog-title">Confirmation</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Are you sure you want to delete entity?
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <BaseButton text={"Cancel"} onClick={() => setModalOpen(false)} />
        <BaseButton
          text={"Confirm"}
          onClick={(e) => {
            setModalOpen(false);
            onConfirm();
          }}
        />
      </DialogActions>
    </Dialog>
  );
};
export default DialogModal;
