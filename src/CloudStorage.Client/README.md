## 客户端部署

Android 部署
创建android密钥
```shell
keytool -genkey -v -keystore myapp.keystore -alias key -keyalg RSA -keysize 2048 -validity 10000
```

构建android
```shell
dotnet publish -f:net6.0-android -c:Release /p:AndroidSigningKeyPass=mypassword /p:AndroidSigningStorePass=mypassword
```