===================
=   SpicyLand     =
===================

# Spicyland Project

Il progetto Spicyland è un'applicazione che utilizza Docker per la containerizzazione e Azure Kubernetes Service (AKS) per l'orchestrazione dei container. Questo documento fornisce istruzioni dettagliate per la configurazione e l'esecuzione dell'applicazione.

## Sommario

- [Caratteristiche](#caratteristiche)
- [Prerequisiti](#prerequisiti)
- [Installazione](#installazione)
- [Esecuzione con Docker](#esecuzione-con-docker)
- [Gestione del Cluster AKS](#gestione-del-cluster-aks)

## Caratteristiche

- Containerizzazione dell'applicazione con Docker
- Configurazione e gestione di un cluster AKS su Azure
- Supporto per la gestione delle immagini Docker su Docker Hub

## Prerequisiti

- [Docker](https://www.docker.com/get-started)
- [Azure CLI](https://docs.microsoft.com/it-it/cli/azure/install-azure-cli)
- Un account [Docker Hub](https://hub.docker.com/)
- Un account [Azure](https://azure.microsoft.com/)

## Installazione

1. **Clonare il repository:**

    ```bash
    git clone https://github.com/solutionsbytegenius/SpicyLand.git
    cd spicyland
    ```

2. **Costruire l'immagine Docker:**

    ```bash
    docker build -t spicyland .
    ```

## Esecuzione con Docker

1. **Eseguire il container Docker:**

    ```bash
    docker run -d -p 8080:80 spicyland
    ```

2. **Eseguire un container Docker configurato per MSSQL:**

    ```bash
    docker run --network netspicyland --ip 172.18.0.2 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SistemiCloud2023@" -e "MSSQL_PID=Evaluation" -p 27123:1433 -v C:\Images\:/app/wwwroot/Images1 --name dbspicyland --hostname dbspicyland -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
    ```

3. **Taggare l'immagine Docker:**

    ```bash
    docker tag spicyland andreas995/spicyland:latest
    ```

4. **Eseguire il push dell'immagine su Docker Hub:**

    ```bash
    docker push andreas995/spicyland:latest
    ```

## Gestione del Cluster AKS

1. **Autenticarsi ad Azure:**

    ```bash
    az login
    ```

2. **Creare il cluster AKS:**

    ```bash
    az aks create --resource-group ReSpicyland --name SpicylandCluster --node-count 2 --node-vm-size "Standard_B2s" --enable-addons monitoring --generate-ssh-keys
    ```

3. **Ottenere le credenziali del cluster AKS:**

    ```bash
    az aks get-credentials --admin --name SpicylandCluster --resource-group ReSpicyland
    ```

4. **Ottenere informazioni specifiche sul cluster AKS:**

    ```bash
    az aks show --resource-group ReSpicyland --name SpicylandCluster --query "addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName" -o table
    ```

5. **Elencare gli indirizzi IP pubblici:**

    ```bash
    az network public-ip list --output table
    ```

6. **Eseguire file KuberSpycyland.yaml**

    ```bash
    kubectl apply -f KuberSpycyland.yaml
    ```

7. **Visualizzare lo stato dei pod**

    ```bash
    kubectl get pod
    ```
8. **Visualizzare i servizi nel cluster**

    ```bash
    kubectl get svc
    ```
    
9. **Visualizzare i deployment nel cluster**

    ```bash
    kubectl get deployments
    ```
    
===================
=   SpicyLand     =
===================
