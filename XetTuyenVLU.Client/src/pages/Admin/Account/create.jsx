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
import { useNavigate, useParams } from "react-router-dom";
import { CreateAccount, GetAllRoles } from "../../../services/accountService";
import { toast } from "react-toastify";

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

const AccountCreate = (props) => {
  const classes = useStyles();
  const navigate = useNavigate();
  let { id } = useParams();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    defaultValues: {
      hoVaTen: "",
      tenDangNhap: "",
      matKhau: "",
      vaiTroId: "",
    },
  });
  const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
  const [roles, setRoles] = useState([{}]);

  const onSubmit = (data) => {
    props.setIsLoading(true);
    const formData = new FormData();
    formData.append("hoVaTen", data.hoVaTen);
    formData.append("tenDangNhap", data.tenDangNhap);
    formData.append("matKhau", data.matKhau);
    formData.append("vaiTroId", data.vaiTroId);
    CreateAccount(formData)
      .then((response) => {
        if (response.data) {
          setTimeout(() => {
            props.setIsLoading(false);
            navigate(-1);
            toast.success("T???o t??i kho???n m???i th??nh c??ng!", {
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
    GetAllRoles()
      .then((response) => {
        setRoles(response.data);
      })
      .catch((error) => console.log(error));
  }, []);

  return (
    <Container className={classes.container}>
      <Form className={classes.form} onSubmit={handleSubmit(onSubmit, onError)}>
        <div style={{ textAlign: "center" }}>
          <h3 className={classes.title}>T???o m???i t??i kho???n</h3>
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
                {...register("tenDangNhap", {
                  required: {
                    value: true,
                    message: "Email l?? b???t bu???c",
                  },
                  pattern: {
                    value: emailPattern,
                    message: "Email kh??ng h???p l???",
                  },
                })}
              />
              <div style={{ fontSize: "13px", color: "#777" }}>
                (????y s??? l?? t??n ????ng nh???p)
              </div>
              {errors && (
                <div className={classes.error}>
                  {errors.tenDangNhap?.message}
                </div>
              )}
            </Col>
          </Row>
        </FormGroup>
        <FormGroup>
          <Row>
            <Label className={classes.label} for="matKhau" md={3}>
              M???t kh???u
            </Label>
            <Col style={{ paddingLeft: "25px" }} md={7}>
              <Input
                className={classes.fieldInput}
                placeholder="M???t kh???u"
                id="matKhau"
                name="matKhau"
                type="password"
                {...register("matKhau", {
                  required: {
                    value: true,
                    message: "M???t kh???u l?? b???t bu???c",
                  },
                })}
              />
              {errors && (
                <div className={classes.error}>{errors.matKhau?.message}</div>
              )}
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
                {...register("vaiTroId", {
                  required: {
                    value: true,
                    message: "Vai tr?? l?? b???t bu???c",
                  },
                })}
              >
                <option value="">-- Ch???n vai tr?? --</option>
                {roles &&
                  roles.map((data, index) => (
                    <option key={index} value={data.id}>
                      {data.tenVaiTro}
                    </option>
                  ))}
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
            X??C NH???N
          </Button>
          <Button className={classes.cancel} onClick={() => navigate(-1)}>
            QUAY L???I
          </Button>
        </div>
      </Form>
    </Container>
  );
};

export default AccountCreate;
