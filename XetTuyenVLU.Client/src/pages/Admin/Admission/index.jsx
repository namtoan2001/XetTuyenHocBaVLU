import React, { useEffect, useState, Component } from "react";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ManageAccountsOutlined from "@mui/icons-material/ManageAccountsOutlined";
import { Link } from "react-router-dom";
import { createUseStyles } from "react-jss";
import {
  GetDataForHoso,
  GetDataForBangDiem,
  DeleteDataForHosoThpt,
  DeleteBangDiemForBangDiemThpt,
  GetPhase,
  ReceiveAdmissionProfileById,
  RejectAdmissionProfileById,
} from "../../../services/admissionAdminService";
import { DataGrid } from "@mui/x-data-grid";
import { toast } from "react-toastify";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";
import { Container, Button, Input } from "reactstrap";
import moment from "moment";
import DoneIcon from "@mui/icons-material/Done";
import CloseIcon from "@mui/icons-material/Close";
import * as XLSX from "xlsx";
import { Switch } from "@mui/material";

const useStyles = createUseStyles({
  containerAdmin: {
    padding: "50px 0px",
    marginBottom: "150px",
    width: "100%",
    height: "700px",
  },
  containerCollaborator: {
    padding: "50px 180px",
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
  btnExport: {
    backgroundColor: "#2c9b91",
    margin: "20px 0",
    border: "none",
    marginLeft: "0",
    "&:hover": {
      backgroundColor: "#3bccbf",
    },
  },
  fieldInput: {
    fontSize: "15px",
    width: "150px",
  },
});

const AdmissionAdmin = (props) => {
  const classes = useStyles();
  const [DataForHoso, setDataForHoso] = useState([]);
  const [DataForBangDiem, setDataForBangDiem] = useState([]);
  const [phase, setPhase] = useState([]);

  const fetchDataAdmission = () => {
    props.setIsLoading(true);
    GetDataForHoso()
      .then((response) => {
        var list = response.data.map((data) => {
          return {
            ...data,
            ngaySinh: moment(data.ngaySinh).format("DD/MM/YYYY"),
            tenNganhTenToHop1: data.tenNganhTenToHop1?.split("#")[0],
            tenNganhTenToHop2: data.tenNganhTenToHop2?.split("#")[0],
            tenNganhTenToHop3: data.tenNganhTenToHop3?.split("#")[0],
            quocTich: data.quocTich?.split("|")[1],
            dotId: null,
            khoa: null,
            phase: data.phase,
            daGuiMail: data.daGuiMail === true ? "???? g???i" : "Ch??a g???i"
          };
        });
        setDataForHoso(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) =>
        setTimeout(() => {
          props.setIsLoading(false);
          toast.error(error.response.data, {
            theme: "colored",
          });
        }, 1000)
      );
  };

  const exportData = () => {
    props.setIsLoading(true);
    GetDataForHoso()
      .then((response) => {
        var list = response.data.map((data) => {
          return {
            ...data,
            ngaySinh: moment(data.ngaySinh).format("DD/MM/YYYY"),
            tenNganhTenToHop1: data.tenNganhTenToHop1?.split("#")[0],
            tenNganhTenToHop2: data.tenNganhTenToHop2?.split("#")[0],
            tenNganhTenToHop3: data.tenNganhTenToHop3?.split("#")[0],
            quocTich: data.quocTich?.split("|")[1],
            dotId: null,
            khoa: null,
            phase: data.phase,
            daGuiMail: data.daGuiMail === true ? "???? g???i" : "Ch??a g???i" 
          };
        });
        setDataForHoso(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
      });
    GetDataForBangDiem()
      .then((response) => {
        var list = response.data.map((data) => {
          return {
            ...data,
            dotId: null,
          };
        });
        setDataForBangDiem(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleOnExport = () => {
    props.setIsLoading(true);
    const wb = XLSX.utils.book_new();
    const HeaderHoSo = [
      [
        "ID",
        "H??? V?? T??N",
        "EMAIL",
        "CMND/CCCD",
        "S??? ??I???N THO???I",
        "N??I SINH",
        "GI???I T??NH",
        "NG??Y SINH",
        "D??N T???C",
        "T??N GI??O",
        "QU???C T???CH",
        "H??? KH???U",
        "N??M T???T NGHI???P",
        "S??? B??O DANH",
        "H???C L???C L???P 12",
        "H???NH KI???M L???P 12",
        "LO???I H??NH T???T NGHI???P",
        "T??N TR?????NG THPT",
        "T??N L???P 12",
        "KHU V???C",
        "?????I T?????NG ??U TI??N",
        "CH???NG CH??? NGO???I NG???",
        "T??N NG??NH X??T TUY???N 1",
        "CH????NG TR??NH H???C",
        "T??N NG??NH X??T TUY???N 2",
        "CH????NG TR??NH H???C",
        "T??N NG??NH X??T TUY???N 3",
        "CH????NG TR??NH H???C",
        "?????A CH??? LI??N L???C",
        "TR???NG TH??I H??? S??",
        "TR???NG TH??I G???I MAIL",
        "??I???M M??? THU???T",
        "??I???M N??NG KHI???U",
        "?????T X??T TUY???N",
      ],
    ];
    var wscols2 = [
      { width: 7 },
      { width: 30 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
      { width: 25 },
    ]
    const HeaderBangDiem =[
      [
        "ID",
        "H??? V?? T??N",
        "??I???M TO??N C??? N??M L???P 11",
        "??I???M V??N C??? N??M L???P 11",
        "??I???M ANH C??? N??M L???P 11",
        "??I???M PH??P C??? N??M L???P 11",
        "??I???M L?? C??? N??M L???P 11",
        "??I???M H??A C??? N??M L???P 11",
        "??I???M SINH C??? N??M L???P 11",
        "??I???M S??? C??? N??M L???P 11",
        "??I???M ?????A C??? N??M L???P 11",
        "??I???M GDCD C??? N??M L???P 11",
        "??I???M TO??N H???C K?? I L???P 12",
        "??I???M V??N H???C K?? I L???P 12",
        "??I???M ANH H???C K?? I L???P 12",
        "??I???M PH??P H???C K?? I L???P 12",
        "??I???M L?? H???C K?? I L???P 12",
        "??I???M H??A H???C K?? I L???P 12",
        "??I???M SINH H???C K?? I L???P 12",
        "??I???M S??? H???C K?? I L???P 12",
        "??I???M ?????A H???C K?? I L???P 12",
        "??I???M GDCD H???C K?? I L???P 12",
        "??I???M TO??N C??? N??M L???P 12",
        "??I???M V??N C??? N??M L???P 12",
        "??I???M ANH C??? N??M L???P 12",
        "??I???M PH??P C??? N??M L???P 12",
        "??I???M L?? C??? N??M L???P 12",
        "??I???M H??A C??? N??M L???P 12",
        "??I???M SINH C??? N??M L???P 12",
        "??I???M S??? C??? N??M L???P 12",
        "??I???M ?????A C??? N??M L???P 12",
        "??I???M GDCD C??? N??M L???P 12",
      ]
    ]
    var wscols = [
      { width: 7 },
      { width: 30 },
      { width: 40 },
      { width: 20 },
      { width: 20 },
      { width: 30 },
      { width: 10 },
      { width: 30 },
      { width: 10 },
      { width: 10 },
      { width: 15 },
      { width: 80 },
      { width: 10 },
      { width: 20 },
      { width: 10 },
      { width: 10 },
      { width: 10 },
      { width: 40 },
      { width: 10 },
      { width: 10 },
      { width: 10 },
      { width: 40 },
      { width: 40 },
      { width: 20 },
      { width: 40 },
      { width: 20 },
      { width: 40 },
      { width: 20 },
      { width: 20 },
      { width: 15 },
      { width: 15 },
      { width: 15 },
      { width: 15 },
    ];
    var ws_data = [
    ];
    const ws = XLSX.utils.json_to_sheet(ws_data);
    var ws_data2 = [
    ];
    const ws2 = XLSX.utils.json_to_sheet(ws_data2);
    ws["!cols"] = wscols;
    ws2["!cols"] = wscols2;
    XLSX.utils.sheet_add_aoa(ws, HeaderHoSo);
    XLSX.utils.sheet_add_aoa(ws2, HeaderBangDiem);
    if (DataForHoso.length != 0) {
      XLSX.utils.sheet_add_json(ws, DataForHoso, {
        origin: "A2",
        skipHeader: true,
      });
      XLSX.utils.sheet_add_json(ws2, DataForBangDiem, {
        origin: "A2",
        skipHeader: true,
      });
      XLSX.utils.book_append_sheet(wb, ws, "HoSo");
      XLSX.utils.book_append_sheet(wb, ws2, "BangDiem");
      XLSX.writeFile(wb, "hoso.xlsx");
      setTimeout(() => props.setIsLoading(false), 1000);
      toast.success("Xu???t d??? li???u th??nh c??ng!", { theme: "colored" });
    } else {
      setTimeout(() => props.setIsLoading(false), 1000);
      toast.error("Kh??ng c?? d??? li???u ????? xu???t", { theme: "colored" });
    }
  };

  const handleDelete = async (Id) => {
    confirmAlert({
      title: "X??c nh???n x??a",
      message: "B???n c?? ch???c ch???n mu???n x??a d??? li???u n??y kh??ng?",
      buttons: [
        {
          label: "X??a",
          onClick: () => {
            DeleteDataForHosoThpt(Id);
            DeleteBangDiemForBangDiemThpt(Id);
            GetDataForHoso()
              .then((response) => {
                setDataForHoso(
                  response.data.filter((data) => data.id !== Id),
                  toast.success("X??a d??? li???u th??nh c??ng!", { theme: "colored" })
                );
              })
              .catch((error) => {
                toast.error(error.response.data, { theme: "colored" });
              });
          },
        },
        {
          label: "Kh??ng",
        },
      ],
    });
  };

  const handleOnSelect = (event) => {
    props.setIsLoading(true);
    GetDataForHoso()
      .then((response) => {
        var list = [];
        response.data.map((data) => {
          if (event.target.value == data.dotId) {
            list.push({
              ...data,
              ngaySinh: moment(data.ngaySinh).format("DD/MM/YYYY"),
              tenNganhTenToHop1: data.tenNganhTenToHop1?.split("#")[0],
              tenNganhTenToHop2: data.tenNganhTenToHop2?.split("#")[0],
              tenNganhTenToHop3: data.tenNganhTenToHop3?.split("#")[0],
              quocTich: data.quocTich?.split("|")[1],
              dotId: null,
              khoa: null,
              phase: data.phase,
              daGuiMail: data.daGuiMail === true ? "???? g???i" : "Ch??a g???i" 
            });
          } else if (event.target.value == "all") {
            list.push({
              ...data,
              ngaySinh: moment(data.ngaySinh).format("DD/MM/YYYY"),
              tenNganhTenToHop1: data.tenNganhTenToHop1?.split("#")[0],
              tenNganhTenToHop2: data.tenNganhTenToHop2?.split("#")[0],
              tenNganhTenToHop3: data.tenNganhTenToHop3?.split("#")[0],
              quocTich: data.quocTich?.split("|")[1],
              dotId: null,
              khoa: null,
              phase: data.phase,
              daGuiMail: data.daGuiMail === true ? "???? g???i" : "Ch??a g???i" 
            });
          }
        });
        setDataForHoso(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
        setTimeout(() => props.setIsLoading(false), 1000);
      });
    GetDataForBangDiem()
      .then((response) => {
        var list = [];
        response.data.map((data) => {
          if (event.target.value == data.dotId) {
            list.push({
              ...data,
              dotId: null
            });
          } else if (event.target.value == "all") {
            list.push({
              ...data,
              dotId: null
            });
          }
        });
        setDataForBangDiem(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleReceiveAdmissionProfile = (id) => {
    if (id !== 0) {
      props.setIsLoading(true);
      ReceiveAdmissionProfileById(id)
        .then((response) => {
          if (response.data) {
            setTimeout(() => {
              props.setIsLoading(false);
              toast.success(`Nh???n h??? s?? x??t tuy???n th??nh c??ng!`, {
                theme: "colored",
              });
            }, 1000);
            fetchDataAdmission();
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
    }
  };

  const handleRejectAdmissionProfile = (id) => {
    if (id !== 0) {
      props.setIsLoading(true);
      RejectAdmissionProfileById(id)
        .then((response) => {
          if (response.data) {
            setTimeout(() => {
              props.setIsLoading(false);
              toast.success(`T??? ch???i nh???n h??? s?? x??t tuy???n th??nh c??ng!`, {
                theme: "colored",
              });
            }, 1000);
            fetchDataAdmission();
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
    }
  };

  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 100,
      hide: props.role === "Admin" ? false : true,
    },
    {
      field: "hoVaTen",
      headerName: "H??? T??n",
      width: props.role === "Admin" ? 170 : 250,
    },
    {
      field: "gioiTinh",
      headerName: "Gi???i t??nh",
      width: 100,
    },
    {
      field: "ngaySinh",
      headerName: "Ng??y sinh",
      width: props.role === "Admin" ? 100 : 150,
    },
    {
      field: "tenNoiSinh",
      headerName: "?????a Ch???",
      width: props.role === "Admin" ? 150 : 250,
    },
    {
      field: "cmnd",
      headerName: "S??? CMND",
      width: 120,
    },
    {
      field: "dienThoaiDd",
      headerName: "S??? ??i???n tho???i",
      width: 120,
      hide: props.role === "Admin" ? false : true,
    },
    {
      field: "email",
      headerName: "Email",
      width: 110,
      hide: props.role === "Admin" ? false : true,
    },
    {
      field: "daGuiMail",
      headerName: "???? g???i mail",
      width: 100,
      hide: props.role === "Admin" ? false : true,
    },
    {
      field: "daNhanHoSo",
      headerName: "???? nh???n",
      type: "actions",
      width: 100,
      hide: props.role === "Admin" ? false : true,
      renderCell: (params) => (
        <React.Fragment>
          <Switch
            checked={
              DataForHoso.find((x) => x.id === params.id).daNhanHoSo === "N"
                ? true
                : false
            }
            onChange={(e) => {
              e.target.checked
                ? handleReceiveAdmissionProfile(params.id)
                : handleRejectAdmissionProfile(params.id);
            }}
          />
        </React.Fragment>
      ),
    },
    {
      field: "tuychon",
      headerName: "T??y ch???n",
      width: 100,
      type: "actions",
      hide: props.role === "Admin" ? false : true,
      renderCell: (params) => {
        return (
          <>
            <Link to={"/xettuyen-vlu-admin/admission/edit/" + params.id}>
              <ManageAccountsOutlined sx={{ color: "#3bb077" }} />
            </Link>
            <div style={{ paddingLeft: "20px" }}>
              <DeleteForeverIcon
                sx={{ color: "red" }}
                onClick={() => handleDelete(params.id)}
              />
            </div>
          </>
        );
      },
    },
  ];
  useEffect(() => {
    exportData();
    handleOnSelect();
    GetPhase()
      .then((response) => {
        setPhase(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);
  useEffect(() => {
    props.setIsLoading(true);
    GetDataForHoso()
      .then((response) => {
        var list = response.data.map((data) => ({
          ...data,
          ngaySinh: moment(data.ngaySinh).format("DD/MM/YYYY"),
          tenNganhTenToHop1: data.tenNganhTenToHop1?.split("#")[0],
          tenNganhTenToHop2: data.tenNganhTenToHop2?.split("#")[0],
          tenNganhTenToHop3: data.tenNganhTenToHop3?.split("#")[0],
          quocTich: data.quocTich?.split("|")[1],
          dotId: null,
          khoa: null,
          phase: data.phase,
          daGuiMail: data.daGuiMail === true ? "???? g???i" : "Ch??a g???i" 
        }));
        setDataForHoso(list);
        setTimeout(() => props.setIsLoading(false), 1000);
      })
      .catch((error) => {
        console.log(error);
        setTimeout(() => props.setIsLoading(false), 1000);
      });
  }, []);

  if (props.role === "Admin" ? false : true) {
    return (
      <Container
        className={
          props.role === "Admin"
            ? classes.containerAdmin
            : classes.containerCollaborator
        }
      >
        <h2 className={classes.title}>Qu???n l?? h??? s?? x??t tuy???n</h2>
        <DataGrid
          className={classes.table}
          rows={DataForHoso}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[5]}
          // checkboxSelection
        />
      </Container>
    );
  } else {
    return (
      <Container
        className={
          props.role === "Admin"
            ? classes.containerAdmin
            : classes.containerCollaborator
        }
      >
        <h2 className={classes.title}>Qu???n l?? h??? s?? x??t tuy???n</h2>
        <Button className={classes.btnExport} onClick={handleOnExport}>
          Xu???t d??? li???u ra Excel
        </Button>
        <Input
          className={classes.fieldInput}
          type="select"
          onChange={handleOnSelect}
        >
          <option value="all">T???t c??? c??c ?????t</option>
          {phase.map((data, index) => (
            <option key={index} value={data.id}>
              ?????t {data.dotThu} Kh??a {data.khoa}
            </option>
          ))}
        </Input>
        <DataGrid
          className={classes.table}
          rows={DataForHoso}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[5]}
          // checkboxSelection
        />
      </Container>
    );
  }
};
export default AdmissionAdmin;
