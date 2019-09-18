# configure ingress SSL on aks
https://docs.microsoft.com/en-us/azure/aks/ingress-tls

## install helm client
choco install kubernetes-helm -y

## install nginx ingress controller
kubectl apply -f rbac-tiller.yaml
helm init --history-max 200 --service-account tiller --node-selectors "beta.kubernetes.io/os=linux"
helm install stable/nginx-ingress --namespace default --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux
kubectl --namespace default get services --selector app=nginx-ingress

## set ip dnsname in aks
set IP= 
set DNSNAME=
az network public-ip list --query "[?ipAddress!=null]|[?contains(ipAddress, '%IP%')].[id]" --output tsv>PUBLICIP.txt
SET /P PUBLICIPID=<PUBLICIP.txt
az network public-ip update --ids %PUBLICIPID% --dns-name %DNSNAME%
az network public-ip show --ids %PUBLICIPID% --query "{fqdn: dnsSettings.fqdn, address: ipAddress}"

## set let's encrypt
kubectl -n default apply -f https://raw.githubusercontent.com/jetstack/cert-manager/release-0.8/deploy/manifests/00-crds.yaml
kubectl create namespace cert-manager
kubectl label namespace cert-manager certmanager.k8s.io/disable-validation=true
helm repo add jetstack https://charts.jetstack.io
helm repo update
helm install --name cert-manager --namespace cert-manager --version v0.8.0 jetstack/cert-manager
kubectl -n default apply -f cluster-issuer.yaml

## set ingress 
kubectl -n default apply -f railway-ingress.yaml

## get tls-secret
kubectl describe certificate tls-secret