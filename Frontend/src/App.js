import React, { useEffect, useState } from "react";
import "./Form.css"
// import JSONPretty from 'react-json-pretty';
import 'react-json-pretty/themes/monikai.css';
import Box from '@mui/material/Box';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import TreeView from '@mui/lab/TreeView';
import TreeItem from '@mui/lab/TreeItem';
import data from "./hierarc.json"
// import { ControlledTreeEnvironment, Tree, StaticTreeDataProvider } from 'react-complex-tree';


function App() {
  const [ selectedClusteringOptions, setSelectedClusteringOptions ] = useState([]);
  const [ clusteringOptions, setclusteringOptions ] = useState([]);
  const [ algoritmOptions, setAlgoritmOptions ] = useState("kmeans-iterations");
  const [ iterationOptions, setIterationOptions ] = useState(5);
  const [ kOptions, setkOptions ] = useState(5);
  const [expanded, setExpanded] = React.useState([]);
  let int = 0;



  useEffect(() => {
    const createTree = (node) => {
      int++
      return(
          <>
          {node && node.left && Object.keys(node.left).length &&
            <TreeItem key={"left"+(int+1).toString()} nodeId={"left"+(int+1).toString()}>
              {createTree(node.left)}
            </TreeItem>
          }
          {node && node.right && Object.keys(node.right).length &&
            <TreeItem key={"right"+(int+2).toString()} nodeId={"right"+(int+1).toString()}>
              {createTree(node.right)}
            </TreeItem>
          }
          {node && node.blog && node.blog.blogTitle && Object.keys(node.blog.blogTitle).length &&
            <TreeItem key={"blog"+(int+3).toString()} nodeId={"blog"+(int+1).toString()} label={node.blog.blogTitle}></TreeItem>
          }
          </>
      )
    }

    const renderBody = () => {
      if(clusteringOptions &&  clusteringOptions.length > 1)
      {
        return clusteringOptions.map((cluster, id)=> {
          return(
          <TreeItem key={"cluster"+cluster.id} nodeId={"cluster"+cluster.id} label={"Cluster " + id + " (" + cluster.assignments.length + ")"}>
            {cluster.assignments.map((element, index) => (
              <TreeItem key={"index-"+index+"x"+cluster.id}nodeId={"index-"+index+"x"+cluster.id} label={"- "+element.blogTitle}></TreeItem>
            ))}
          </TreeItem>
          )
        })
      } else if(clusteringOptions.length !== 0){
          return createTree(clusteringOptions[0])
        // return (<JSONPretty className ="response"  data={clusteringOptions} name="data" />)
      }
    }

    setSelectedClusteringOptions(renderBody)
  }, [clusteringOptions, int]);



  const handleAlgoritmChange = (e) => {
    setAlgoritmOptions(e.target.value);
  };

  const handleIterationChange = (e) => {
    setIterationOptions(e.target.value);
  };

  const handleKChange = (e) => {
    setkOptions(e.target.value);
  };

  const handleToggle = (event, nodeIds) => {
    setExpanded(nodeIds);
  };

  const useClustering = async (e) => {
    e.preventDefault();

    let clustering;
    if(algoritmOptions !== "hierarchical")
    {
      clustering = await fetch(`https://localhost:5001/clustering/${algoritmOptions}?k=${iterationOptions}&iterations=${kOptions}`);
      clustering = await clustering.json()
    }
    else clustering = data;

    setclusteringOptions(clustering);
  };


  return (
    <div>
      <header>
        <h1>Clustering</h1>
      </header>
      <form className = "form">
        <div>
          <label>Method:</label>
            <select onChange={handleAlgoritmChange}>
              <option key="kmeansiterations" value="kmeans-iterations">K-means Clustering (iterations)</option>
              <option key="kmeansself" value="kmeans-self">K-means Clustering (self stopping)</option>
              <option key="hierarchical" value="hierarchical">Hierarchical Clustering</option>
            </select>
        </div>
        <div>
          <label>K:</label>
            <input type="k" min={1} max={10} value={kOptions} onChange={handleKChange}>
            </input>
        </div>

        <div>
          <label>Iterations:</label>
            <input type="number" min={1} max={10} value={iterationOptions} onChange={handleIterationChange}>
            </input>
        </div>

        <div>
          <button className ="submitButton" onClick={useClustering}>
            Find clusters
          </button>
        </div>
      </form>

      {/* <ControlledTreeEnvironment items={selectedClusteringOptions} getItemTitle={item => selectedClusteringOptions} viewState={{}}>
      <Tree treeId="tree-1" rootItem="centroids" treeLabel="Tree Example" />
      </ControlledTreeEnvironment> */}

      <Box className ="response" sx={{ height: 270, flexGrow: 1, maxWidth: 400, overflowY: 'auto' }}>
        <Box sx={{ mb: 1 }}>
      </Box>
      <TreeView
        aria-label="controlled"
        disableSelection={true}
        disabledItemsFocusable={true}
        multiSelect={false}
        defaultCollapseIcon={<ExpandMoreIcon />}
        defaultExpandIcon={<ChevronRightIcon />}
        expanded={expanded}
        onNodeToggle={handleToggle}
      >
      {selectedClusteringOptions}
      </TreeView>
      </Box>
    </div>
  );
}

export default App;
