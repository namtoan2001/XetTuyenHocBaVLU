import React, {useState, useRef } from "react";
import { createUseStyles } from "react-jss";
import {
    Container,
    Button,
    Form,
    Input,
} from "reactstrap";
import { useLocation,Navigate } from "react-router-dom";
import {AddImgPathHocBa} from "../../services/profileService"
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";

const useStyles = createUseStyles({
    container: {
        padding: "2 20px",
        paddingLeft: "50px",
        backgroundColor: "white",
        maxWidth: "95%",
        minHeight: "680px",
    },

    centerBlueText: {
        textAlign: "center",
        fontSize: "26px",
        fontWeight: "bold",
        color: "#097fd9",
    },

    centerGraySmallText: {
        textAlign: "center",
        fontSize: "15px",
        fontWeight: "bold",
        color: "gray",
    },

    center: {
        textAlign: "center",
    },

    button: {
        textAlign: "center",
        margin: "5px",
        backgroundColor: "#26a69a",
        "&:hover": {
            backgroundColor: "#1f897f",
        },
    },

    button2: {
        textAlign: "center",
        margin: "5px",
        backgroundColor: "#3394f5",
        "&:hover": {
            backgroundColor: "#2877c7",
        },
        color: "white",
    },

    notifactionText: {
        textAlign: "center",
        fontSize: "30px",
        fontWeight: "bold",
        color: "red",
    },
    imgHocBa: {
        width: "300px",
    }
});

const initialFieldValues = [{
    imgSource: '',
    imgFile: [],
}
]
function Upload(props) {
    const classes = useStyles();
    const location = useLocation();
    const [validate, setValidate] = React.useState(true);
    const [values, setValues] = useState(initialFieldValues);
    const inputRef = useRef(null);
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
      } = useForm({
        defaultValues: {
          imgFile: [],
        },
      });
    const MAX_LENGTH = 10;
    const showPreview = (e) => {
      props.setIsLoading(true);
      if(Array.from(e.target.files).length > MAX_LENGTH){
        toast.error(`Kh??ng th??? upload qu?? ${MAX_LENGTH} t???p tin`,{theme: "colored"})
        setValidate(true);
        inputRef.current.value = null;
        setTimeout(() => props.setIsLoading(false), 1000);
      }
      else{
        let img = [];

        for (let i = 0; i < e.target.files.length; i++) {
          if(e.target.files[i].size > 10485760){
            toast.error('K??ch c??? file qu?? l???n', {theme: "colored"})
            inputRef.current.value = null;
            setValidate(true);
            setTimeout(() => props.setIsLoading(false), 1000);
          }
          else if(!e.target.files[i].name.match(/\.(jpg|jpeg|png)$/)) {
            toast.error('File kh??ng ????ng ?????nh d???ng', {theme: "colored"})
            setValidate(true);
            inputRef.current.value = null;
            setTimeout(() => props.setIsLoading(false), 1000);
            break;
          }
          else{
            img.push({
              imgSource: URL.createObjectURL(e.target.files[i]),
              imgFile: e.target.files[i],
            });
            setValidate(false);
          }
          setValues({
            imgSource: img.map((data) => data.imgSource),
            maHoSoThpt: location.state.MaHoSoThpt,
            imgFile: img.map((data) => data.imgFile),
          });
          setTimeout(() => props.setIsLoading(false), 1000);
          }
      }
      setTimeout(() => props.setIsLoading(false), 1000);
    }
    const onSubmit = () =>{
      props.setIsLoading(true);
      console.log(values.imgFile);
      const formData = new FormData();
        if(values.imgFile > MAX_LENGTH){
          setTimeout(() => props.setIsLoading(false), 1000);
          setValidate(true);
          toast.error(`Kh??ng th??? upload qu?? ${MAX_LENGTH} t???p tin`,{theme: "colored"})
        }
        else if(values.imgFile==null){
          setTimeout(() => props.setIsLoading(false), 1000);
          setValidate(true);
          toast.error("Ch??a ch???n t???p tin n??o",{theme: "colored"})
        }
        else{
          for(const key of Object.keys(values.imgFile)){
            formData.append("maHoSoThpt", values.maHoSoThpt);
            formData.append('imgFile', values.imgFile[key])
          }
          setValidate(false);
          AddImgPathHocBa(formData)
          setTimeout(() => props.setIsLoading(false), 1000);
          inputRef.current.value = null;
          toast.success("B???n ???? upload th??nh c??ng!", { theme: "colored" });
        }
    }
  
    if(!location.state) {
      return <Navigate to="/hosocuatoi"/>
    }
    return (
      <Container className={classes.container}>
        <br></br>

        <div className={classes.centerBlueText}>
          UPLOAD h???c b???/ phi???u ??i???m, CMND/CCCD ONLINE
        </div>
        <div className={classes.centerGraySmallText}>
          (C?? th??? ch???n 1 hay nhi???u file. Mu???n ch???n nhi???u h??n 1 file vui l??ng gi???
          ph??m CTRL r???i ch???n h??nh ti???p theo)
        </div>
        <br></br>
        <Form onSubmit={handleSubmit(onSubmit)}>
          <div className={classes.center}>
            <div className={classes.centerGraySmallText}>
              <input
                ref={inputRef}
                type="file"
                accept="image/*"
                onChange={showPreview}
                id="imgFile"
                name="imgFile"
                multiple
                required
              />
            </div>
            <Button type="submit" disabled={validate} className={classes.button}>
              N???p h??nh ???? ch???n
            </Button>
          </div>
        </Form>
      </Container>
    );
};

export default Upload

