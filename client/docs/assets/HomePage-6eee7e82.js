import{_ as y,w as k,b as d,k as l,A as o,h as g,P as u,r as c,o as n,c as r,a as s,F as w,e as x,f as i,g as m}from"./index-797a150f.js";const K={setup(){k(()=>{_()});async function _(){try{await l.getKeeps()}catch(e){u.error(e)}}return{keeps:d(()=>o.keeps),user:d(()=>o.user),async getMyVaults(e){try{await l.setActiveKeep(e),o.user.isAuthenticated&&await g.getMyVaults()}catch(a){u.error(a)}}}}},C={class:"container-fluid"},M={class:"row justify-content-center"},V={class:"col-12 col-md-8"},b={class:"masonry-container"},A=["onClick"];function P(_,e,a,p,B,S){const h=c("KeepCard"),f=c("KeepDetails"),v=c("ModalWrapper");return n(),r("div",C,[s("section",M,[s("div",V,[s("div",b,[(n(!0),r(w,null,x(p.keeps,t=>(n(),r("div",{class:"text-light mb-3 text-center",key:t.id},[i(v,{id:"show-keep-details"},{button:m(()=>[s("div",{onClick:$=>p.getMyVaults(t.id)},[i(h,{keep:t},null,8,["keep"])],8,A)]),body:m(()=>[i(f,{keep:t},null,8,["keep"])]),_:2},1024)]))),128))])])])])}const E=y(K,[["render",P],["__scopeId","data-v-2004aec7"]]);export{E as default};