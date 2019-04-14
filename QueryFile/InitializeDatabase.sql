﻿CREATE DATABASE QUANLYCAPHE
GO

USE QUANLYCAPHE
GO

CREATE TABLE NGUYENLIEU
(
	MANGUYENLIEU INT IDENTITY(1,1) PRIMARY KEY,
	TINHTRANG BIT NOT NULL DEFAULT 0 --0: CÒN, 1: HẾT
)
GO

CREATE TABLE NHACUNGCAP
(
	MANHACUNGCAP INT IDENTITY(1,1) PRIMARY KEY,
	TENNHACUNGCAP NVARCHAR(50) NOT NULL,
	DIACHI NVARCHAR(100) NOT NULL,
	SODIENTHOAI VARCHAR(11) NOT NULL,
	EMAIL VARCHAR(20) DEFAULT NULL,
	GHICHU NVARCHAR(20) DEFAULT NULL
)
GO

CREATE TABLE CHITIETNGUYENLIEU
(
	MANGUYENLIEU INT UNIQUE NOT NULL,
	TENNGUYENLIEU NVARCHAR(50) NOT NULL,
	MANHACUNGCAP INT NOT NULL,
	DONGIA FLOAT NOT NULL DEFAULT 0 CHECK(DONGIA>=0),
	DONVI NVARCHAR(10) NOT NULL ,
	SOLUONGTON INT NOT NULL DEFAULT 0 CHECK(SOLUONGTON>=0),
	GHICHU NVARCHAR(20) DEFAULT NULL,
	CONSTRAINT FK_CTNL_NL FOREIGN KEY (MANGUYENLIEU) REFERENCES DBO.NGUYENLIEU(MANGUYENLIEU) ON DELETE CASCADE,
	CONSTRAINT FK_CTNL_NCC FOREIGN KEY (MANHACUNGCAP) REFERENCES DBO.NHACUNGCAP(MANHACUNGCAP) ON DELETE CASCADE
)
GO

CREATE TABLE DANHMUC
(
	MADANHMUC INT IDENTITY(1,1) PRIMARY KEY,
	TENDANHMUC NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE THUCDON
(
	MADOUONG INT IDENTITY(1,1) PRIMARY KEY,
	MADANHMUC INT NOT NULL,
	TENDOUONG NVARCHAR(50) NOT NULL,
	GIABAN FLOAT NOT NULL DEFAULT 0 CHECK(GIABAN>=0),
	TINHTRANG BIT DEFAULT 1, --0: CÓ THỂ ORDER, 1: KHÔNG THỂ ORDER
	HINHANH IMAGE,
	CONSTRAINT FK_TD_DM FOREIGN KEY (MADANHMUC) REFERENCES DBO.DANHMUC(MADANHMUC) ON DELETE CASCADE
)
GO

CREATE TABLE CHITIETTHUCDON
(
	MADOUONG INT NOT NULL,
	MANGUYENLIEU INT NOT NULL,
	CONSTRAINT FK_CTTD_TD FOREIGN KEY (MADOUONG) REFERENCES DBO.THUCDON(MADOUONG),
	CONSTRAINT FK_CTTD_NL FOREIGN KEY (MANGUYENLIEU) REFERENCES DBO.NGUYENLIEU(MANGUYENLIEU) ON DELETE CASCADE
)
GO

CREATE TABLE TAIKHOAN
(
	TENDANGNHAP VARCHAR(10) PRIMARY KEY, --KHI TẠO TK PHẢI KIỂM TRA TENDANGNHAP ĐÃ TỒN TẠI HAY CHƯA
	MATKHAU NVARCHAR(1000) NOT NULL DEFAULT 1 
)
GO

CREATE TABLE CHUCVU
(
	MACHUCVU INT IDENTITY(1,1) PRIMARY KEY,
	TENCHUCVU NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE CHUCNANG --BẢNG CHỨC NĂNG LÀ MẶC ĐỊNH DO LẬP TRÌNH VIÊN ĐẶT CHỨC NĂNG CHO NGƯỜI DÙNG CHỌN
(
	MACHUCNANG INT IDENTITY(1,1) PRIMARY KEY,
	TENCHUCNANG NVARCHAR(20) UNIQUE NOT NULL,
	GHICHU NVARCHAR(50) DEFAULT NULL
)
GO

CREATE TABLE PHANQUYEN
(
	MACHUCVU INT NOT NULL,
	MACHUCNANG INT NOT NULL,
	CONSTRAINT FK_PQ_CV FOREIGN KEY (MACHUCVU) REFERENCES DBO.CHUCVU(MACHUCVU) ON DELETE CASCADE, 
	CONSTRAINT FK_PQ_CN FOREIGN KEY (MACHUCNANG) REFERENCES DBO.CHUCNANG(MACHUCNANG) ON DELETE CASCADE
)
GO

CREATE TABLE NHANVIEN
(
	MANHANVIEN INT IDENTITY(1,1) PRIMARY KEY,
	TENDANGNHAP	VARCHAR(10)	UNIQUE NOT NULL,
	MACHUCVU INT NOT NULL,
	HOTEN NVARCHAR(50) NOT NULL,
	GIOITINH BIT DEFAULT 0,
	CHUNGMINHNHANDAN VARCHAR(10) UNIQUE NOT NULL,
	DIACHI NVARCHAR(100) NOT NULL,
	SODIENTHOAI VARCHAR(11)	NOT NULL,
	ANHDAIDIEN IMAGE,
	CONSTRAINT FK_NV_TK FOREIGN KEY (TENDANGNHAP) REFERENCES DBO.TAIKHOAN(TENDANGNHAP) ON DELETE CASCADE,
	CONSTRAINT FK_NV_CV FOREIGN KEY (MACHUCVU) REFERENCES DBO.CHUCVU(MACHUCVU) ON DELETE CASCADE
)
GO

CREATE TABLE BAN
(
	MABAN INT IDENTITY(1,1) PRIMARY KEY,
	TENBAN NVARCHAR(20) NOT NULL,
	TRANGTHAI BIT NOT NULL DEFAULT 0
)
GO

CREATE TABLE HOADON
(
	MAHOADON INT IDENTITY(1,1) PRIMARY KEY,
	MABAN INT NOT NULL,
	TRANGTHAI BIT NOT NULL DEFAULT 0,
	GIAMGIA FLOAT NOT NULL DEFAULT 0 CHECK(GIAMGIA>=0),
	TONGTIEN FLOAT NOT NULL DEFAULT 0 CHECK(TONGTIEN>=0),
	GHICHU NVARCHAR(20) DEFAULT NULL,
	CONSTRAINT FK_HD_B FOREIGN KEY (MABAN) REFERENCES DBO.BAN(MABAN) ON DELETE NO ACTION ON UPDATE NO ACTION
)
GO

CREATE TABLE CHITIETHOADON
(
	MAHOADON INT NOT NULL,
	MADOUONG INT NOT NULL,
	THOIGIANLAP DATETIME NOT NULL DEFAULT GETDATE(),
	THOIGIANKET DATETIME DEFAULT NULL,
	SOLUONG INT NOT NULL DEFAULT 0 CHECK(SOLUONG>=0),
	THANHTIEN FLOAT NOT NULL DEFAULT 0  CHECK(THANHTIEN>=0),
	CONSTRAINT FK_CTHD_HD FOREIGN KEY (MAHOADON) REFERENCES DBO.HOADON(MAHOADON) ON DELETE CASCADE,
	CONSTRAINT FK_CTHD_TD FOREIGN KEY (MADOUONG) REFERENCES DBO.THUCDON(MADOUONG) ON DELETE NO ACTION ON UPDATE NO ACTION
)
GO

-----
DROP TABLE PHANQUYEN
GO
DROP TABLE CHUCNANG
GO
DROP TABLE NHANVIEN
GO
DROP TABLE TAIKHOAN
GO
DROP TABLE CHUCVU
GO
DROP TABLE CHITIETNGUYENLIEU
GO
DROP TABLE CHITIETHOADON
GO
DROP TABLE CHITIETTHUCDON
GO
DROP TABLE THUCDON
GO
DROP TABLE NHACUNGCAP
GO
DROP TABLE NGUYENLIEU
GO
DROP TABLE HOADON
GO
DROP TABLE DANHMUC
GO
DROP TABLE BAN
GO