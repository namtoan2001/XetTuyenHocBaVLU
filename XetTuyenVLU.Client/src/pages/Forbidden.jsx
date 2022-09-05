import React from "react";
import { createUseStyles } from "react-jss";
import { Container } from "reactstrap";

const useStyles = createUseStyles({
  container: {
    backgroundColor: "#FFF",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    border: "4px solid #D0D0D0",
    borderRadius: "4px",
    marginTop: "50px",
    marginBottom: "550px",
  },
  header: {
    fontSize: "25px",
    color: "#CC0000",
    fontWeight: "bold",
    padding: "20px",
  },
  content: {
    fontWeight: "bold",
    fontSize: "22px",
    paddingBottom: "20px",
  },
});

const Forbidden = (props) => {
  const classes = useStyles();

  return (
    <Container className={classes.container}>
      <div className={classes.header}>403 - Forbidden</div>
      <div className={classes.content}>
        Bạn không được phép truy cập vào trang này.
      </div>
    </Container>
  );
};

export default Forbidden;
