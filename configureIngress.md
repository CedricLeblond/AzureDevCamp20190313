# configure ingress SSL on aks
https://docs.microsoft.com/en-us/azure/aks/ingress-tls

## install helm client
choco install kubernetes-helm -y

## install nginx ingress controller
helm init
helm install stable/nginx-ingress --namespace default --set controller.replicaCount=2 --set rbac.create=false
kubectl --namespace default get services -o wide -w terrific-stoat-nginx-ingress-controller

## set ip dnsname in aks
set IP=137.117.163.136
az network public-ip list --query "[?ipAddress!=null]|[?contains(ipAddress, '%IP%')].[id]" --output tsv>PUBLICIP.txt
SET /P PUBLICIPID=<PUBLICIP.txt
az network public-ip update --ids %PUBLICIPID% --dns-name %DNSNAME%
az network public-ip show --ids %PUBLICIPID% --query "{fqdn: dnsSettings.fqdn, address: ipAddress}"

## set let's encrypt
kubectl label namespace default certmanager.k8s.io/disable-validation=true
kubectl -n default apply -f https://raw.githubusercontent.com/jetstack/cert-manager/release-0.6/deploy/manifests/00-crds.yaml
helm install stable/cert-manager --namespace default --set ingressShim.defaultIssuerName=letsencrypt-staging --set ingressShim.defaultIssuerKind=ClusterIssuer --set rbac.create=false --set serviceAccount.create=false --version v0.6.0
kubectl -n default apply -f cluster-issuer.yaml

## set ingress 
kubectl -n default apply -f hassancehef-ingress.yaml

## get tls-secret
kubectl describe certificate tls-secret