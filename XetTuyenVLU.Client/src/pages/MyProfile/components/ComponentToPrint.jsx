import React, { useEffect } from "react";
import { createUseStyles } from "react-jss";
import {
  Container,
  Form,
  Button,
  FormGroup,
  Input,
  Label,
  Col,
  Row,
  Collapse,
} from "reactstrap";
import logo from "../../../images/logo.png";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";

const useStyles = createUseStyles({
  container: {
    padding: "2 20px",
    paddingLeft: "50px",
    backgroundColor: "white",
    maxWidth: "100%",
    minHeight: "680px",
  },

  wrapper: {
    maxWidth: "100%",
  },

  tableTitle: {
    color: "#c61d23",
    fontSize: "24px",
    fontWeight: "bold",
    paddingBottom: "5px",
  },
  subTitle: {
    color: "#2F39B1",
    fontSize: "18px",
    fontWeight: "bold",
  },
  containerForm: {
    backgroundColor: "#FFF",
    marginBottom: "50px",
    padding: "10px 20px",
  },
  section: {
    fontSize: "17px",
    backgroundColor: "red",
    color: "#FFF",
    padding: "0 15px",
    marginTop: "10px",
  },
  subSection: {
    fontSize: "15px",
    fontWeight: "bold",
    color: "#097fd9",
    padding: "5px 0",
  },
  line: {
    border: 0,
    borderTop: "1px solid #9e8787",
    margin: "10px 0",
    marginBottom: "30px",
  },
  label: {
    textAlign: "right",
    fontSize: "15px",
  },
  fieldInput: {
    fontSize: "15px",
  },
  require: {
    color: "red",
    paddingLeft: "5px",
  },
  labelRadio: {
    fontSize: "15px",
    marginLeft: "5px",
    marginRight: "20px",
    marginBottom: 0,
  },

  labelSmall: {
    fontSize: "14px",
    fontWeight: "500",
  },

  redText: {
    fontSize: "18px",
    fontWeight: "bold",
    color: "red",
  },

  boldText: {
    fontSize: "13px",
    fontWeight: "bold",
  },

  smallText: {
    fontSize: "13px",
  },

  table: {
    textAlign: "left",
    borderColor: "#bfbfbf",
    border: "1px solid",
    margin: "10px 0",
  },

  button: {
    padding: "7px 20px",
    border: 0,
    backgroundColor: "#26a69a",
    "&:hover": {
      backgroundColor: "#1f897f",
    },
  },

  tableTitle: {
    textAlign: "center",
    width: "20%",
    borderColor: "#bfbfbf",
    minWidth: "100px",
    border: "1px solid",
  },

  tableCenter: {
    textAlign: "center",
    borderColor: "#bfbfbf",
    minWidth: "100px",
    border: "1px solid",
    fontSize: "13px",
  },

  buttonContainer: {
    textAlign: "center",
  },

  button1: {
    margin: "5px",
    backgroundColor: "#26a69a",
    "&:hover": {
      backgroundColor: "#1f897f",
    },
  },

  button2: {
    margin: "5px",
    backgroundColor: "#d4a304",
    "&:hover": {
      backgroundColor: "#997603",
    },
  },

  button3: {
    margin: "5px",
    backgroundColor: "#3394f5",
    "&:hover": {
      backgroundColor: "#2877c7",
    },
  },
});

export const ComponentToPrint = React.forwardRef((props, ref) => {
  const classes = useStyles();
  const location = useLocation();
  const [showTableOption1, setShowTableOption1] = React.useState(false);
  const [showTableOption2, setShowTableOption2] = React.useState(false);

  const ShowTableOption1 = () => (
    <table className={classes.table}>
      <tbody>
        <tr>
          <th className={classes.tableTitle}>M??N H???C</th>
          <th className={classes.tableCenter}>TO??N</th>
          <th className={classes.tableCenter}>V??N</th>
          <th className={classes.tableCenter}>ANH</th>
          <th className={classes.tableCenter}>PH??P</th>
          <th className={classes.tableCenter}>L??</th>
          <th className={classes.tableCenter}>H??A</th>
          <th className={classes.tableCenter}>SINH</th>
          <th className={classes.tableCenter}>S???</th>
          <th className={classes.tableCenter}>?????A</th>
          <th className={classes.tableCenter}>GDCD</th>
        </tr>
        <tr>
          <td
            className={classes.boldText}
            style={{ border: "1px solid", borderColor: "#bfbfbf" }}
          >
            ??i???m TB n??m h???c l???p 11
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Toan}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Van}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Anh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Phap}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Ly}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Hoa}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Sinh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Su}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Dia}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam11Gdcd}
          </td>
        </tr>
        <tr>
          <td
            className={classes.boldText}
            style={{ border: "1px solid", borderColor: "#bfbfbf" }}
          >
            ??i???m TB HK1 l???p 12
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Toan}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Van}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Anh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Phap}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Ly}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Hoa}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Sinh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Su}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Dia}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemHK1Lop12Gdcd}
          </td>
        </tr>
      </tbody>
    </table>
  );

  const ShowTableOption2 = () => (
    <table className={classes.table}>
      <tbody>
        <tr>
          <th className={classes.tableTitle}>M??N H???C</th>
          <th className={classes.tableCenter}>TO??N</th>
          <th className={classes.tableCenter}>V??N</th>
          <th className={classes.tableCenter}>ANH</th>
          <th className={classes.tableCenter}>PH??P</th>
          <th className={classes.tableCenter}>L??</th>
          <th className={classes.tableCenter}>H??A</th>
          <th className={classes.tableCenter}>SINH</th>
          <th className={classes.tableCenter}>S???</th>
          <th className={classes.tableCenter}>?????A</th>
          <th className={classes.tableCenter}>GDCD</th>
        </tr>
        <tr>
          <td
            className={classes.boldText}
            style={{ border: "1px solid", borderColor: "#bfbfbf" }}
          >
            ??i???m TB n??m h???c l???p 12
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Toan}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Van}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Anh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Phap}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Ly}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Hoa}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Sinh}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Su}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Dia}
          </td>
          <td className={classes.tableCenter}>
            {location.state.diemCaNam12Gdcd}
          </td>
        </tr>
      </tbody>
    </table>
  );

  useEffect(() => {
    if (location.state.diemCaNam11Toan != null) {
      setShowTableOption1(true);
      setShowTableOption2(false);
    } else {
      setShowTableOption1(false);
      setShowTableOption2(true);
    }
  }, []);

  return (
    <Container className={classes.container}>
      <div className={classes.container} ref={ref}>
        <div className={classes.wrapper}>
          <br></br>

          <Row>
            <Col>
              <Link to="/">
                <img src={logo} />
              </Link>
            </Col>
            <Col></Col>
            <Col>
              <p
                style={{
                  textAlign: "center",
                  fontWeight: "bold",
                  fontSize: "15px",
                }}
              >
                C???NG H??A X?? H???I CH??? NGH??A VI???T NAM
              </p>
              <p
                style={{
                  textAlign: "center",
                  fontWeight: "bold",
                  fontSize: "15px",
                }}
              >
                ?????c L???p - T??? Do - H???nh Ph??c
              </p>
            </Col>
          </Row>
          <p
            style={{
              textAlign: "center",
              fontWeight: "bold",
              fontSize: "22px",
              color: "red",
            }}
          >
            ????NG K?? X??T TUY???N 2022 - ?????T 1
          </p>
          <p style={{ textAlign: "center", fontSize: "15px" }}>
            (D??nh cho th?? sinh x??t k???t qu??? h???c b??? THPT)
          </p>
          <Row>
            <Col>
              <div className={classes.subSection}>TH??NG TIN TH?? SINH</div>
            </Col>
            <Col></Col>
            <Col></Col>
            <Col>
              <span className={classes.smallText}>M?? H??? S??: </span>
              <span className={classes.redText}>DVL_{location.state.id}</span>
            </Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>1. H??? t??n th?? sinh:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.hoVaTen}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>Gi???i t??nh: </span>
              <span className={classes.smallText}>
                {location.state.gioiTinh}
              </span>
            </Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>2. Ng??y th??ng n??m sinh:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.ngaySinh}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>3. N??i sinh: </span>
              <span className={classes.smallText}>
                {location.state.tenNoiSinh}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>4. D??n t???c: </span>
              <span className={classes.smallText}>
                {location.state.tenDanToc}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>5. T??n gi??o: </span>
              <span className={classes.smallText}>
                {location.state.tenTonGiao}
              </span>
            </Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>
                6. S??? ch???ng minh d??n d??n/ C??n c?????c c??ng d??n:
              </span>
            </Col>
            <Col>
              <span className={classes.smallText}>{location.state.cmnd} -</span>
              <span className={classes.boldText}> Qu???c t???ch: </span>
              <span className={classes.smallText}>
                {location.state.quocTich}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>7. H??? kh???u th?????ng tr??:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.hoKhauTenQH}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col></Col>
            <Col>
              <span className={classes.boldText}>Ph?????ng x??: </span>
              <span className={classes.smallText}>
                {location.state.hoKhauTenPhuong}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>Qu???n/Huy???n: </span>
              <span className={classes.smallText}>
                {location.state.hoKhauTenQh}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>T???nh/Th??nh ph???: </span>
              <span className={classes.smallText}>
                {location.state.hoKhauTenTinhTp}
              </span>
            </Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>8. N??m t???t nghi???p THPT:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.namTotNghiep}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>9. H???c l???c l???p 12:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.hocLucLop12}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>10. H???nh ki???m l???p 12:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.hanhKiemLop12}
              </span>
            </Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>
                11. Th?? sinh h???c ch????ng tr??nh:
              </span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.loaiHinhTn}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>12. N??i h???c THPT l???p 12:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.maTruongThpt} - {location.state.tenTruongThpt} -{" "}
              </span>
              <span className={classes.boldText}>L???p: </span>
              <span className={classes.smallText}>
                {location.state.tenLop12}
              </span>
            </Col>
            <Col></Col>
            <Col>
              <span className={classes.boldText}>M?? t???nh: </span>
              <span>{location.state.truongThptTenTinhTp}</span>
            </Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>13. Khu v???c:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>{location.state.khuVuc}</span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>14. ?????i t?????ng ??u ti??n:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>
                {location.state.doiTuongUuTien}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <p></p>
          <Row>
            <Col>
              <div className={classes.subSection}>TH??NG TIN X??T TUY???N</div>
            </Col>
            <Col>
              <span className={classes.boldText}>Ph????ng ??n x??t tuy???n: </span>
              <span className={classes.smallText}>{location.state.phuongan}</span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          {showTableOption1 ? <ShowTableOption1 /> : null}
          {showTableOption2 ? <ShowTableOption2 /> : null}
          <Row>
            <Col>
              <span className={classes.boldText}>Ch???ng ch??? ngo???i ng???:</span>{" "}
              <span className={classes.smallText}>
                (n???u c??) {location.state.ccnn}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>
                Ng??nh ????ng k?? x??t tuy???n 1:{" "}
              </span>{" "}
              <span className={classes.smallText}>
                {location.state.tenNganh1}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? ng??nh: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet1MaNganh}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? t??? h???p: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet1MaToHop}
              </span>
            </Col>
          </Row>
          <Row style={{ paddingBottom: "10px" }}>
            <Col>
              <span className={classes.smallText}>
                Ch????ng tr??nh h???c: {location.state.chuongTrinhHoc1}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>
                Ng??nh ????ng k?? x??t tuy???n 2:{" "}
              </span>{" "}
              <span className={classes.smallText}>
                {location.state.tenNganh2}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? ng??nh: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet2MaNganh}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? t??? h???p: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet2MaToHop}
              </span>
            </Col>
          </Row>
          <Row style={{ paddingBottom: "10px" }}>
            <Col>
              <span className={classes.smallText}>
                Ch????ng tr??nh h???c: {location.state.chuongTrinhHoc2}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col>
              <span className={classes.boldText}>
                Ng??nh ????ng k?? x??t tuy???n 3:{" "}
              </span>{" "}
              <span className={classes.smallText}>
                {location.state.tenNganh3}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? ng??nh: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet3MaNganh}
              </span>
            </Col>
            <Col>
              <span className={classes.boldText}>M?? t??? h???p: </span>{" "}
              <span className={classes.smallText}>
                {location.state.xet3MaToHop}
              </span>
            </Col>
          </Row>
          <Row style={{ paddingBottom: "10px" }}>
            <Col>
              <span className={classes.smallText}>
                Ch????ng tr??nh h???c: {location.state.chuongTrinhHoc3}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <p></p>
          <Row>
            <Col>
              <div className={classes.subSection}>TH??NG TIN LI??N H???</div>
            </Col>
            <Col></Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col md={1}>
              <span className={classes.boldText}>?????a ch??? li??n h???:</span>
            </Col>
            <Col md={4}>
              <span className={classes.smallText}>
                {location.state.hoKhauTenPhuong}, {location.state.hoKhauTenQH},{" "}
                {location.state.hoKhauTenTinhTp}
              </span>
            </Col>
            <Col></Col>
            <Col></Col>
          </Row>
          <Row>
            <Col md={1}>
              <span className={classes.boldText}>??i???n tho???i:</span>
            </Col>
            <Col md={3}>
              <span className={classes.smallText}>
                {location.state.dienThoaiDd} -{" "}
              </span>
              <span className={classes.boldText}>??i???n tho???i ph??? huynh: </span>
              <span className={classes.smallText}>
                {location.state.dienThoaiPhuHuynh}
              </span>
            </Col>
            <Col md={1}>
              <span className={classes.boldText}>Email:</span>
            </Col>
            <Col>
              <span className={classes.smallText}>{location.state.email}</span>
            </Col>
          </Row>
          <br></br>
          <Row>
            <Col>
              <div style={{ columnSpan: "7" }}>
                <table style={{ borderStyle: "dashed", borderWidth: "1px" }}>
                  <tbody>
                    <tr>
                      <td>
                        <b>H??? s?? g???m c??</b>:
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <ul style={{ listStyleType: "none" }}>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span> H???c b???
                            THPT (b???n photocopy c??ng ch???ng);
                          </li>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span> B???ng
                            t???t nghi???p THPT/Gi???y ch???ng nh???n TN t???m th???i (b???n
                            photocopy c??ng ch???ng);
                          </li>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span> ??i???m
                            thi n??ng khi???u;
                          </li>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span> Ch???ng
                            ch??? ngo???i ng??? (b???n photocopy c??ng ch???ng);
                          </li>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span>{" "}
                            CMND/CCCD (b???n photocopy c??ng ch???ng);
                          </li>
                          <li>
                            <span style={{ fontSize: "1.5em" }}>???</span> Gi???y
                            ch???ng nh???n ??u ti??n.
                          </li>
                        </ul>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </Col>
            <Col>
              <div style={{ textAlign: "center", paddingLeft: "150px" }}>
                Ng??y ....... th??ng ....... n??m 2022
                <br></br>
                <b>Ch??? k?? c???a th?? sinh</b>
                <br></br>
                <i>(K?? v?? ghi r?? h??? t??n)</i>
                <p></p>
                <br></br>
                <br></br>
                <br></br>
                <br></br>
                <br></br>
                {location.state.hoVaTen}
              </div>
            </Col>
          </Row>
        </div>
      </div>
    </Container>
  );
});
