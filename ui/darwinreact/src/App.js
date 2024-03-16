import './App.css';
//import CreatePlayList from './components/Playlist/CreatePlayList';
//import PlayListDetail from './components/Playlist/PlayListDetail';
import Signin from './components/Signin';
//import Register from './components/Register'
//import ContentIndex from './components/Content/Index';
//import CreateContent from './components/Content/CreateContent';
//import ContentDetail from './components/Content/ContentDetail';
//import CategoryDetail from './components/Category/CategoryDetail';
//import CategoryList from './components/Category/CategoryList';
//import CreateCategory from './components/Category/CreateCategory'; 
//import MoodList from './components/mood/MoodList';
import PlayListIndex from './components/Playlist/Index'

function App() {
  return (
    <div className="App">
      {/* <CategoryList></CategoryList>  */}
      {/* <CreateCategory></CreateCategory> */}
       {/* <CreateMood></CreateMood> */}
      {/* <MoodList></MoodList> */}
       <Signin></Signin> 
      {/*<Register></Register>*/}
      {/* <ContentIndex></ContentIndex> */}
      {/* <CreateContent></CreateContent>  */}
      {/* <CategoryDetail></CategoryDetail> */}
      {/* <ContentDetail></ContentDetail>  */}
      {/* <PlayListDetail></PlayListDetail> */}
      {/* <CreatePlayList></CreatePlayList> */}
      <PlayListIndex></PlayListIndex>
    </div>
  );
}

export default App;
