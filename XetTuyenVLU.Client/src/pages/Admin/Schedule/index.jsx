import React, { useEffect, useState } from "react";
import { Container, Button } from "reactstrap";
import { DataGrid } from "@mui/x-data-grid";
import { createUseStyles } from "react-jss";
import moment from "moment";
import { Switch } from "@mui/material";
import DialogConfirm from "../../../components/DialogConfirm";
import { toast } from "react-toastify";
import DoneIcon from "@mui/icons-material/Done";
import CloseIcon from "@mui/icons-material/Close";
import CreateScheduleModal from "./components/CreateScheduleModal";
import {
  ChangeStatusSchedule,
  GetAllPhasesNotExpiry,
  GetAllSchedules,
  GetCategoriesForSchedule,
  ValidateAllSchedulesWereExpired,
} from "../../../services/scheduleService";

const useStyles = createUseStyles({
  container: {
    padding: "50px 40px",
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

const Schedule = (props) => {
  const classes = useStyles();
  const [schedules, setSchedules] = useState([]);
  const [phases, setPhases] = useState([]);
  const [categories, setCategories] = useState([]);
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

  const fetchAllSchedules = () => {
    props.setIsLoading(true);
    GetAllSchedules()
      .then((response) => {
        var list = response.data.map((data) => {
          return {
            ...data,
            ngayBatDau: moment(data.ngayBatDau).format("DD/MM/YYYY"),
            ngayKetThuc: moment(data.ngayKetThuc).format("DD/MM/YYYY"),
            ngayTao: moment(data.ngayTao).format("DD/MM/YYYY"),
          };
        });
        setSchedules(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        setTimeout(() => props.setIsLoading(false), 1000);
        console.log("error", error);
      });
  };

  const fetchAllPhasesNotExpiry = () => {
    props.setIsLoading(true);
    GetAllPhasesNotExpiry()
      .then((response) => {
        setPhases(response.data);
        setTimeout(() => props.setIsLoading(false), 1000);
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

  const fetchCategoriesForSchedule = () => {
    props.setIsLoading(true);
    GetCategoriesForSchedule()
      .then((response) => {
        setCategories(response.data);
        setTimeout(() => props.setIsLoading(false), 1000);
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

  const handleOpenDialog = (id) => {
    setId(id);
    setContent(
      `B???n c?? ch???c ch???n mu???n chuy???n tr???ng th??i l???ch tr??nh ID ${id} th??nh Active ?`
    );
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setId(0);
    setOpenDialog(false);
  };

  const handleActiveSchedule = () => {
    if (id !== 0) {
      props.setIsLoading(true);
      ChangeStatusSchedule(id)
        .then((response) => {
          if (response.data) {
            setTimeout(() => {
              props.setIsLoading(false);
              toast.success(`Active l???ch tr??nh ID ${id} th??nh c??ng!`, {
                theme: "colored",
              });
            }, 1000);
            setId(0);
            setOpenDialog(false);
            fetchAllSchedules();
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
    console.log(
      "schedules",
      schedules.find((x) => x.id === params.id).isExpired
    );
    return schedules.find((x) => x.id === params.id).isExpired ? (
      <CloseIcon sx={{ color: "red" }} />
    ) : (
      <DoneIcon sx={{ color: "green" }} />
    );
  };

  const columns = [
    { field: "id", headerName: "ID", width: 70 },
    { field: "dotThu", headerName: "?????t th???", width: 100 },
    { field: "khoa", headerName: "Kh??a", width: 100 },
    { field: "tenHinhThuc", headerName: "H??nh th???c", width: 180 },
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
              schedules.find((x) => x.id === params.id).trangThaiId === 1
                ? true
                : false
            }
            disabled={
              schedules.find((x) => x.id === params.id).trangThaiId === 1
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
    ValidateAllSchedulesWereExpired();
    fetchAllSchedules();
    fetchAllPhasesNotExpiry();
    fetchCategoriesForSchedule();
  }, []);

  return (
    <Container className={classes.container}>
      <DialogConfirm
        title="Th??ng b??o chuy???n tr???ng th??i"
        content={content}
        open={openDialog}
        handleClose={handleCloseDialog}
        onConfirm={handleActiveSchedule}
      />
      {openModal ? (
        <CreateScheduleModal
          open={openModal}
          phases={phases}
          categories={categories}
          handleClose={handleCloseModal}
          setIsLoading={props.setIsLoading}
          fetchAllSchedules={fetchAllSchedules}
          ValidateAllSchedulesWereExpired={ValidateAllSchedulesWereExpired}
        />
      ) : (
        ""
      )}
      <h2 className={classes.title}>L???ch s??? ch???nh s???a l???ch tr??nh</h2>
      <Button className={classes.buttonCreate} onClick={handleOpenModal}>
        T???o m???i
      </Button>
      <DataGrid
        className={classes.table}
        rows={schedules}
        columns={columns}
        pageSize={10}
      />
    </Container>
  );
};

export default Schedule;
