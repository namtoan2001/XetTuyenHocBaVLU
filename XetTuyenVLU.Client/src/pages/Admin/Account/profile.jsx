import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { createUseStyles } from "react-jss";
import {
  Button,
  Col,
  Container,
  Form,
  FormGroup,
  Label,
  Row,
} from "reactstrap";
import Input from "../../../components/CustomInput";
import { useNavigate } from "react-router-dom";
import {
  EditAccountProfile,
  GetAccountProfile,
} from "../../../services/accountService";
import { toast } from "react-toastify";
import ChangePasswordModal from "./components/ChangePasswordModal";

const useStyles = createUseStyles({
  container: {
    padding: "50px 350px",
    paddingBottom: "300px",
  },
  title: {
    fontSize: "25px",
    fontWeight: "bold",
    paddingBottom: "30px",
    paddingTop: "20px",
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
  password: {
    cursor: "pointer",
    fontStyle: "italic",
    color: "#0000c8",
    "&:hover": {
      textDecoration: "underline",
    },
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

const AccountProfile = (props) => {
  const classes = useStyles();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    defaultValues: {
      hoVaTen: "",
      tenDangNhap: "",
      vaiTroId: "",
    },
  });
  const [account, setAccount] = useState({});
  const [openModal, setOpenModal] = useState(false);

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const onSubmit = (data) => {
    props.setIsLoading(true);
    const formData = new FormData();
    formData.append("hoVaTen", data.hoVaTen);

    EditAccountProfile(formData)
      .then((response) => {
        if (response.data) {
          setTimeout(() => {
            props.setIsLoading(false);
            props.setUser(data.hoVaTen);
            localStorage.setItem("fullName", data.hoVaTen);
            toast.success("Ch???nh s???a th??ng tin th??nh c??ng!", {
              theme: "colored",
            });
          }, 1000);
        }
      })
      .catch((error) => {
        setTimeout(() => {
          props.setIsLoading(false);
          toast.error(error.response.data, {
            theme: "colored",
          });
        }, 1000);
      });
  };

  const onError = (error) => {
    console.log("error", error);
  };

  useEffect(() => {
    props.setIsLoading(true);
    GetAccountProfile()
      .then((response) => {
        setAccount(response.data);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
        setTimeout(() => props.setIsLoading(false), 1000);
      });
  }, []);

  useEffect(() => {
    if (Object.keys(account).length !== 0) {
      reset(account);
    }
  }, [account]);

  return (
    <Container className={classes.container}>
      <ChangePasswordModal
        open={openModal}
        handleClose={handleCloseModal}
        setIsLoading={props.setIsLoading}
      />
      <Form className={classes.form} onSubmit={handleSubmit(onSubmit, onError)}>
        <div style={{ textAlign: "center" }}>
          <h3 className={classes.title}>Th??ng tin t??i kho???n</h3>
        </div>
        <FormGroup>
          <Row>
            <Label className={classes.label} for="hoVaTen" md={3}>
              H??? v?? t??n
            </Label>
            <Col style={{ paddingLeft: "25px" }} md={7}>
              <Input
                className={classes.fieldInput}
                placeholder="H??? v?? t??n"
                id="hoVaTen"
                name="hoVaTen"
                type="text"
                {...register("hoVaTen", {
                  required: {
                    value: true,
                    message: "H??? v?? t??n l?? b???t bu???c",
                  },
                })}
              />
              {errors && (
                <div className={classes.error}>{errors.hoVaTen?.message}</div>
              )}
            </Col>
          </Row>
        </FormGroup>
        <FormGroup>
          <Row>
            <Label className={classes.label} for="tenDangNhap" md={3}>
              Email
            </Label>
            <Col style={{ paddingLeft: "25px" }} md={7}>
              <Input
                className={classes.fieldInput}
                placeholder="Email"
                id="tenDangNhap"
                name="tenDangNhap"
                type="text"
                disabled
                {...register("tenDangNhap")}
              />
            </Col>
          </Row>
        </FormGroup>
        <FormGroup>
          <Row style={{ display: "flex", alignItems: "center" }}>
            <Label className={classes.label} for="hoVaTen" md={3}>
              M???t kh???u
            </Label>
            <Col style={{ paddingLeft: "25px" }} md={3}>
              <div className={classes.password} onClick={handleOpenModal}>
                ?????i m???t kh???u
              </div>
            </Col>
          </Row>
        </FormGroup>
        <FormGroup>
          <Row>
            <Label className={classes.label} for="vaiTroId" md={3}>
              Vai tr??
            </Label>
            <Col style={{ paddingLeft: "25px" }} md={7}>
              <Input
                className={classes.fieldInput}
                id="vaiTroId"
                name="vaiTroId"
                type="select"
                disabled
                {...register("vaiTroId", {
                  required: {
                    value: true,
                    message: "Vai tr?? l?? b???t bu???c",
                  },
                })}
              >
                <option key={account.vaiTroId} value={account.vaiTroId}>
                  {account.tenVaiTro}
                </option>
              </Input>
              {errors && (
                <div className={classes.error}>{errors.vaiTroId?.message}</div>
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
            L??U THAY ?????I
          </Button>
          <Button className={classes.cancel} onClick={() => navigate(-1)}>
            QUAY L???I
          </Button>
        </div>
      </Form>
    </Container>
  );
};

export default AccountProfile;
