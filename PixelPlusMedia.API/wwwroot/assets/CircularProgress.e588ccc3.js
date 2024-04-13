import{a as N,g as I,I as D,s as v,c,_ as o,J as _,r as U,u as j,f as z,j as g,i as F,k as K}from"./index.dad713cd.js";function W(r){return N("MuiCircularProgress",r)}I("MuiCircularProgress",["root","determinate","indeterminate","colorPrimary","colorSecondary","svg","circle","circleDeterminate","circleIndeterminate","circleDisableShrink"]);const B=["className","color","disableShrink","size","style","thickness","value","variant"];let l=r=>r,P,S,b,$;const t=44,E=D(P||(P=l`
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
`)),G=D(S||(S=l`
  0% {
    stroke-dasharray: 1px, 200px;
    stroke-dashoffset: 0;
  }

  50% {
    stroke-dasharray: 100px, 200px;
    stroke-dashoffset: -15px;
  }

  100% {
    stroke-dasharray: 100px, 200px;
    stroke-dashoffset: -125px;
  }
`)),J=r=>{const{classes:e,variant:s,color:a,disableShrink:d}=r,u={root:["root",s,`color${c(a)}`],svg:["svg"],circle:["circle",`circle${c(s)}`,d&&"circleDisableShrink"]};return K(u,W,e)},L=v("span",{name:"MuiCircularProgress",slot:"Root",overridesResolver:(r,e)=>{const{ownerState:s}=r;return[e.root,e[s.variant],e[`color${c(s.color)}`]]}})(({ownerState:r,theme:e})=>o({display:"inline-block"},r.variant==="determinate"&&{transition:e.transitions.create("transform")},r.color!=="inherit"&&{color:(e.vars||e).palette[r.color].main}),({ownerState:r})=>r.variant==="indeterminate"&&_(b||(b=l`
      animation: ${0} 1.4s linear infinite;
    `),E)),T=v("svg",{name:"MuiCircularProgress",slot:"Svg",overridesResolver:(r,e)=>e.svg})({display:"block"}),V=v("circle",{name:"MuiCircularProgress",slot:"Circle",overridesResolver:(r,e)=>{const{ownerState:s}=r;return[e.circle,e[`circle${c(s.variant)}`],s.disableShrink&&e.circleDisableShrink]}})(({ownerState:r,theme:e})=>o({stroke:"currentColor"},r.variant==="determinate"&&{transition:e.transitions.create("stroke-dashoffset")},r.variant==="indeterminate"&&{strokeDasharray:"80px, 200px",strokeDashoffset:0}),({ownerState:r})=>r.variant==="indeterminate"&&!r.disableShrink&&_($||($=l`
      animation: ${0} 1.4s ease-in-out infinite;
    `),G)),Z=U.exports.forwardRef(function(e,s){const a=j({props:e,name:"MuiCircularProgress"}),{className:d,color:u="primary",disableShrink:M=!1,size:m=40,style:R,thickness:i=3.6,value:f=0,variant:k="indeterminate"}=a,w=z(a,B),n=o({},a,{color:u,disableShrink:M,size:m,thickness:i,value:f,variant:k}),h=J(n),p={},C={},x={};if(k==="determinate"){const y=2*Math.PI*((t-i)/2);p.strokeDasharray=y.toFixed(3),x["aria-valuenow"]=Math.round(f),p.strokeDashoffset=`${((100-f)/100*y).toFixed(3)}px`,C.transform="rotate(-90deg)"}return g(L,o({className:F(h.root,d),style:o({width:m,height:m},C,R),ownerState:n,ref:s,role:"progressbar"},x,w,{children:g(T,{className:h.svg,ownerState:n,viewBox:`${t/2} ${t/2} ${t} ${t}`,children:g(V,{className:h.circle,style:p,ownerState:n,cx:t,cy:t,r:(t-i)/2,fill:"none",strokeWidth:i})})}))}),A=Z;export{A as C};
