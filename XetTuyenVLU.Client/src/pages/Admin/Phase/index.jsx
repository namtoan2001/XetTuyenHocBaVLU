import React, { useEffect, useState } from "react";
import { Container, Button } from "reactstrap";
import { DataGrid } from "@mui/x-data-grid";
import { createUseStyles } from "react-jss";
import {
  ChangeStatusPhase,
  GetAllPhases,
  ValidateAllPhasesWereExpired,
} from "../../../services/phaseService";
import CreatePhaseModal from "./components/CreatePhaseModal";
import moment from "moment";
import { Switch } from "@mui/material";
import DialogConfirm from "../../../components/DialogConfirm";
import { toast } from "react-toastify";
import DoneIcon from "@mui/icons-material/Done";
import CloseIcon from "@mui/icons-material/Close";

const useStyles = createUseStyles({
  container: {
    padding: "50px 130px",
    marginBottom: "150px",
    width: "100%",
    height: "700px",
  },
  title: {
    textAlign: "center",
    fontWeight: "bold",
  },
  table: {
    backgroundColor: "#FFF",
  },
  buttonCreate: {
    backgroundColor: "#2c9b91",
    margin: "20px 0",
    border: "none",
    "&:hover": {
      backgroundColor: "#3bccbf",
    },
  },
});

const Phase = (props) => {
  const classes = useStyles();
  const [phases, setPhases] = useState([]);
  const [openModal, setOpenModal] = useState(false);
  const [id, setId] = useState(0);
  const [openDialog, setOpenDialog] = useState(false);
  const [content, setContent] = useState("");

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const fetchAllPhases = () => {
    props.setIsLoading(true);
    GetAllPhases()
      .then((response) => {
        var list = response.data.map((data) => ({
          ...data,
          ngayBatDau: moment(data.ngayBatDau).format("DD/MM/YYYY"),
          ngayKetThuc: moment(data.ngayKetThuc).format("DD/MM/YYYY"),
          ngayTao: moment(data.ngayTao).format("DD/MM/YYYY"),
        }));
        setPhases(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        setTimeout(() => props.setIsLoading(false), 1000);
        console.log("error", error);
      });
  };

  const handleOpenDialog = (id) => {
    setId(id);
    setContent(
      `B???n c?? ch???c ch???n mu???n chuy???n tr???ng th??i ?????t x??t tuy???n ID ${id} th??nh Active ?`
    );
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setId(0);
    setOpenDialog(false);
  };

  const handleActivePhase = () => {
    if (id !== 0) {
      props.setIsLoading(true);
      ChangeStatusPhase(id)
        .then((response) => {
          if (response.data) {
            setTimeout(() => {
              props.setIsLoading(false);
              toast.success(`Active th??ng b??o ID ${id} th??nh c??ng!`, {
                theme: "colored",
              });
            }, 1000);
            setId(0);
            setOpenDialog(false);
            fetchAllPhases();
          }
        })
        .catch((error) => {
          setTimeout(() => {
            props.setIsLoading(false);
            toast.error(error.response.data, {
              theme: "colored",
            });
          }, 1000);
          setId(0);
          setOpenDialog(false);
        });
    }
  };

  const IsExpiredComponent = ({ params }) => {
    return phases.find((x) => x.id === params.id).isExpired ? (
      <CloseIcon sx={{ color: "red" }} />
    ) : (
      <DoneIcon sx={{ color: "green" }} />
    );
  };

  const columns = [
    { field: "id", headerName: "ID", width: 70 },
    { field: "dotThu", headerName: "?????t th???", width: 100 },
    { field: "khoa", headerName: "Kh??a", width: 100 },
    { field: "ngayBatDau", headerName: "Ng??y b???t ?????u", width: 150 },
    { field: "ngayKetThuc", headerName: "Ng??y k???t th??c", width: 150 },
    { field: "tenTrangThai", headerName: "Tr???ng th??i", width: 100, hide: true },
    {
      field: "isExpired",
      headerName: "Hi???u l???c",
      type: "actions",
      width: 100,
      hide: true,
      renderCell: (params) => <IsExpiredComponent params={params} />,
    },
    { field: "tenNguoiTao", headerName: "Ng?????i t???o", width: 200 },
    { field: "ngayTao", headerName: "Ng??y t???o", width: 150 },
    {
      field: "action",
      headerName: "Tr???ng th??i",
      type: "actions",
      sortble: false,
      width: 100,
      renderCell: (params) => (
        <React.Fragment>
          <Switch
            checked={
              phases.find((x) => x.id === params.id).trangThaiId === 1
                ? true
                : false
            }
            disabled={
              phases.find((x) => x.id === params.id).trangThaiId === 1
                ? true
                : false
            }
            onChange={() => handleOpenDialog(params.id)}
          />
        </React.Fragment>
      ),
    },
  ];

  useEffect(() => {
    ValidateAllPhasesWereExpired();
    fetchAllPhases();
  }, []);

  return (
    <Container className={classes.container}>
      <DialogConfirm
        title="Th??ng b??o chuy???n tr???ng th??i"
        content={content}
        open={openDialog}
        handleClose={handleCloseDialog}
        onConfirm={handleActivePhase}
      />
      <CreatePhaseModal
        open={openModal}
        handleClose={handleCloseModal}
        setIsLoading={props.setIsLoading}
        fetchAllPhases={fetchAllPhases}
        ValidateAllPhasesWereExpired={ValidateAllPhasesWereExpired}
      />
      <h2 className={classes.title}>L???ch s??? ?????t x??t tuy???n</h2>
      <Button className={classes.buttonCreate} onClick={handleOpenModal}>
        T???o m???i
      </Button>
      <DataGrid
        className={classes.table}
        rows={phases}
        columns={columns}
        pageSize={10}
      />
    </Container>
  );
};

export default Phase;
