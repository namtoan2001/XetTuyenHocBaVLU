import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { createUseStyles } from "react-jss";
import { Button, Col, Form, FormGroup, Label, Row } from "reactstrap";
import Input from "../../../../components/CustomInput";
import { toast } from "react-toastify";
import { Paper, Modal } from "@mui/material";
import { CreatePhase } from "../../../../services/phaseService";

const useStyles = createUseStyles({
  container: {},
  modal: {},
  title: {
    fontSize: "25px",
    fontWeight: "bold",
    paddingBottom: "30px",
  },
  form: {
    backgroundColor: "#FFF",
    borderRadius: "4px",
    padding: "30px",
    paddingTop: "20px",
  },
  label: {
    textAlign: "right",
  },
  error: {
    fontSize: "14px",
    color: "red",
    paddingTop: "5px",
  },
  submit: {
    backgroundColor: "#2c9b91",
    border: "none",
    "&:hover": {
      backgroundColor: "#3bccbf",
    },
  },
  cancel: {
    backgroundColor: "#ff3037",
    border: "none",
    "&:hover": {
      backgroundColor: "#ff6065",
    },
  },
});

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "600px",
  bgcolor: "background.paper",
  boxShadow: 24,
  borderRadius: "5px",
  p: 4,
};

const CreatePhaseModal = ({
  open,
  handleClose,
  setIsLoading,
  fetchAllPhases,
  ValidateAllPhasesWereExpired,
}) => {
  const classes = useStyles();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    defaultValues: {
      dotThu: "",
      khoa: "",
      ngayBatDau: "",
      ngayKetThuc: "",
    },
  });

  const onSubmit = (data) => {
    setIsLoading(true);
    const formData = new FormData();
    formData.append("dotThu", data.dotThu);
    formData.append("khoa", data.khoa);
    formData.append("ngayBatDau", data.ngayBatDau);
    formData.append("ngayKetThuc", data.ngayKetThuc);
    CreatePhase(formData)
      .then((response) => {
        if (response.data) {
          ValidateAllPhasesWereExpired();
          setTimeout(() => fetchAllPhases(), 500);
          setTimeout(() => {
            setIsLoading(false);
            handleClose();
            reset("");
            toast.success("T???o ?????t x??t tuy???n m???i th??nh c??ng!", {
              theme: "colored",
            });
          }, 1000);
        }
      })
      .catch((error) => {
        setTimeout(() => {
          setIsLoading(false);
          toast.error(error.response.data, {
            theme: "colored",
          });
        }, 1000);
      });
  };

  const onError = (error) => {
    console.log("error", error);
  };

  return (
    <div>
      <Modal
        className={classes.modal}
        open={open}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Paper sx={style}>
          <Form
            className={classes.form}
            onSubmit={handleSubmit(onSubmit, onError)}
          >
            <div style={{ textAlign: "center" }}>
              <h3 className={classes.title}>T???o ?????t x??t tuy???n m???i</h3>
            </div>
            <FormGroup>
              <Row>
                <Label className={classes.label} for="dotThu" md={4}>
                  Th??? t??? ?????t
                </Label>
                <Col style={{ paddingLeft: "25px" }} md={8}>
                  <Input
                    className={classes.fieldInput}
                    placeholder="Th??? t??? ?????t"
                    id="dotThu"
                    name="dotThu"
                    type="number"
                    {...register("dotThu", {
                      required: {
                        value: true,
                        message: "Th??? t??? ?????t l?? b???t bu???c",
                      },
                    })}
                  />
                  {errors && (
                    <div className={classes.error}>
                      {errors.dotThu?.message}
                    </div>
                  )}
                </Col>
              </Row>
            </FormGroup>
            <FormGroup>
              <Row>
                <Label className={classes.label} for="khoa" md={4}>
                  Kh??a
                </Label>
                <Col style={{ paddingLeft: "25px" }} md={8}>
                  <Input
                    className={classes.fieldInput}
                    placeholder="Kh??a"
                    id="khoa"
                    name="khoa"
                    type="number"
                    {...register("khoa", {
                      required: {
                        value: true,
                        message: "Kh??a l?? b???t bu???c",
                      },
                    })}
                  />
                  {errors && (
                    <div className={classes.error}>{errors.khoa?.message}</div>
                  )}
                </Col>
              </Row>
            </FormGroup>
            <FormGroup>
              <Row>
                <Label className={classes.label} for="ngayBatDau" md={4}>
                  Ng??y b???t ?????u
                </Label>
                <Col style={{ paddingLeft: "25px" }} md={8}>
                  <Input
                    className={classes.fieldInput}
                    id="ngayBatDau"
                    name="ngayBatDau"
                    type="date"
                    onKeyDown={(e) => e.preventDefault()}
                    {...register("ngayBatDau", {
                      required: {
                        value: true,
                        message: "Ng??y b???t ?????u l?? b???t bu???c",
                      },
                    })}
                  />
                  {errors && (
                    <div className={classes.error}>
                      {errors.ngayBatDau?.message}
                    </div>
                  )}
                </Col>
              </Row>
            </FormGroup>
            <FormGroup>
              <Row>
                <Label className={classes.label} for="ngayKetThuc" md={4}>
                  Ng??y k???t th??c
                </Label>
                <Col style={{ paddingLeft: "25px" }} md={8}>
                  <Input
                    className={classes.fieldInput}
                    id="ngayKetThuc"
                    name="ngayKetThuc"
                    type="date"
                    onKeyDown={(e) => e.preventDefault()}
                    {...register("ngayKetThuc", {
                      required: {
                        value: true,
                        message: "Ng??y k???t th??c l?? b???t bu???c",
                      },
                    })}
                  />
                  {errors && (
                    <div className={classes.error}>
                      {errors.ngayKetThuc?.message}
                    </div>
                  )}
                </Col>
              </Row>
            </FormGroup>
            <div
              style={{
                display: "flex",
                justifyContent: "space-evenly",
                paddingTop: "50px",
                paddingBottom: "20px",
              }}
            >
              <Button className={classes.submit} type="submit">
                X??C NH???N
              </Button>
              <Button
                className={classes.cancel}
                onClick={() => {
                  reset("");
                  handleClose();
                }}
              >
                QUAY L???I
              </Button>
            </div>
          </Form>
        </Paper>
      </Modal>
    </div>
  );
};

export default CreatePhaseModal;
