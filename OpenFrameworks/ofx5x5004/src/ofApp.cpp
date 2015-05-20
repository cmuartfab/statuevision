#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::setup(){
    
    
    // listen on the given port
	cout << "listening for osc messages on port " << PORT_RECEIVE << "\n";
	receiver.setup(PORT_RECEIVE);
    current_msg_string = 0;

    // listen on the given port
    cout << "listening for osc messages on port " << PORT_SEND << "\n";
    sender.setup(HOST,PORT_SEND);
    current_msg_string = 0;


    ofSetLogLevel(OF_LOG_VERBOSE);
    ofBackground(0, 0);

    ofDisableArbTex(); // we need GL_TEXTURE_2D for our models coords.

    bAnimate = false;
    bAnimateMouse = false;
    animationPosition = 0;
    
    //text related
    
    lttf.loadFont("Ubuntu-Medium.ttf", 24);
    
    //3d model realted
    //
    
    modelDir.listDir("models/");
    modelDir.sort();
    modelCount = modelDir.size();
    cout << "number of models to load: " << ofToString(modelCount) << endl;
    
    //this is to pre-load all the models, but for one it's not necessary and for another it's not working!
    /*
    for(int i = 0; i < 1; i++){
        modelPath =modelDir.getPath(i)+"/mesh/sculpt.obj";
        cout << "now loading: " << modelPath << endl;
        
        models[i].loadModel(modelPath,true);
        cout << "done loading; setting params... " << endl;
        
        models[i].setPosition(ofGetWidth() * 0.5, (float)ofGetHeight() * 0.75 , 0);
        models[i].setLoopStateForAllAnimations(OF_LOOP_NORMAL);
        models[i].setPausedForAllAnimations(true);
        cout << "done... " << endl;
	}
    */
    
    //load the first model
    modelPath = modelDir.getPath(0)+"/sculpt.obj";
    model.loadModel(modelPath, true);
    model.setPosition(ofGetWidth() * 0.5, (float)ofGetHeight() * 0.75 , 0);
    model.setLoopStateForAllAnimations(OF_LOOP_NORMAL);
    model.playAllAnimations();

    
    if(!bAnimate) {
        model.setPausedForAllAnimations(true);
    }
    
    
    //shader related
    #ifdef TARGET_OPENGLES
        shader.load("shadersES2/shader");
    #else
        if(ofIsGLProgrammableRenderer()){
            shader.load("shadersGL3/shader");
        }else{
            shader.load("shadersGL2/shader");
        }
    #endif
    
    //gui related
    modelXInit = ofGetWidth() * 0.5;
    modelXMin = 0;
    modelXMax = ofGetWidth();
    modelYInit = ofGetHeight() * 0.5;
    modelYMin = 0;
    modelYMax = ofGetHeight()*3;
    
    gui.setup("panel"); // most of the time you don't need a name but don't forget to call setup
    gui.add(bendFreq.set( "Bend Frequency", bendFreqInit, bendFreqMin, bendFreqMax ));
	gui.add(bendAmount.set( "Bend Amount", bendAmountInit, bendAmountMin, bendAmountMax ));
	gui.add(bendAngle.set( "Bend Angle", bendAngleInit, bendAngleMin, bendAngleMax ));
    gui.add(twistFreq.set( "Twist Frequency", twistFreqInit, twistFreqMin, twistFreqMax ));
	gui.add(twistAmount.set( "Twist Amount", twistAmountInit, twistAmountMin, twistAmountMax ));
	gui.add(twistHeight.set( "Twist Size", twistHeightInit, twistHeightMin, twistHeightMax ));
	gui.add(rotZ.set( "Rotate", rotZInit, rotZMin, rotZMax));
	gui.add(modelX.set( "X", modelXInit, modelXMin, modelXMax ));
	gui.add(modelY.set( "Y", modelYInit, modelYMin, modelYMax ));
	gui.add(modelZ.set( "Z", modelZInit, modelZMin, modelZMax));
    gui.add(scale.set( "Scale", scaleInit, scaleMin, scaleMax));
    gui.add(textX.set( "Text X", 70, 0, ofGetWidth()));
    gui.add(textY.set( "Text Y", 800, 0, ofGetHeight()));
    gui.add(exposure.set( "Exposure",exposureInit, exposureMin, exposureMax));
//    gui.add(guiShow.set( "Show GUI",guiShowInit, 0, 1));
    gui.add(bright.set( "Bright", brightInit, brightMin, brightMax));

    
    
}

//--------------------------------------------------------------
void ofApp::update(){

    //OSC related
    // check for waiting messages
	while(receiver.hasWaitingMessages()){
		// get the next message
		ofxOscMessage m;
		receiver.getNextMessage(&m);

		if(m.getAddress() == "/2/fader1"){
			bendFreq = ofMap(m.getArgAsFloat(0),0.,1.,bendFreqMin,bendFreqMax);
		}
        
		if(m.getAddress() == "/2/fader2"){
			bendAmount = ofMap(m.getArgAsFloat(0),0.,1.,bendAmountMin,bendAmountMax);
		}

		if(m.getAddress() == "/2/fader3"){
			bendAngle = ofMap(m.getArgAsFloat(0),0.,1.,bendAngleMin,bendAngleMax);
		}

		if(m.getAddress() == "/2/fader4"){
			twistFreq = ofMap(m.getArgAsFloat(0),0.,1.,twistFreqMin,twistFreqMax);
		}

		if(m.getAddress() == "/2/fader5"){
			twistAmount = ofMap(m.getArgAsFloat(0),0.,1.,twistAmountMin,twistAmountMax);
		}

		if(m.getAddress() == "/2/fader6"){
			twistHeight = ofMap(m.getArgAsFloat(0),0.,1.,twistHeightMin,twistHeightMax);
		}

		if(m.getAddress() == "/2/fader7" || m.getAddress() == "/1/fader7"){
			rotZ = ofMap(m.getArgAsFloat(0),0.,1.,rotZMin,rotZMax);
		}

        if(m.getAddress() == "/2/xy1" || m.getAddress() == "/1/xy1") {
            modelX = (ofMap(m.getArgAsFloat(0),0.,1.,modelXMin,modelXMax)+modelXYLast[0]+modelXYLast[2]+modelXYLast[4])/4;
			modelY = (ofMap(m.getArgAsFloat(1),0.,1.,modelYMin,modelYMax)+modelXYLast[1]+modelXYLast[3]+modelXYLast[5])/4;
            modelXYLast[4] = modelXYLast[2];
            modelXYLast[5] = modelXYLast[3];
            modelXYLast[2] = modelXYLast[0];
            modelXYLast[3] = modelXYLast[1];
            modelXYLast[0] = modelX;
            modelXYLast[1] = modelY;
		}
        
        if(m.getAddress() == "/1/xy2" ) {
            bendAmount = ofMap(m.getArgAsFloat(0),0.,1.,bendAmountMin,bendAmountMax);
            twistAmount = ofMap(m.getArgAsFloat(1),0.,1.,twistAmountMin,twistAmountMax);
        }

		if(m.getAddress() == "/2/fader8"){
			modelZ = ofMap(m.getArgAsFloat(0),0.,1.,modelZMin,modelZMax);
		}

		if(m.getAddress() == "/2/fader11" || m.getAddress() == "/1/fader11"){
			scale = ofMap(m.getArgAsFloat(0),0.,1.,scaleMin,scaleMax);
		}
	
		if(m.getAddress() == "/2/fader10" || m.getAddress() == "/1/fader12"){
			exposure = ofMap(m.getArgAsFloat(0),0.,1.,exposureMin,exposureMax);
		}
        
        if(m.getAddress() == "/2/toggle2" || m.getAddress() == "/1/toggle2"){
			guiShow = m.getArgAsInt32(0);
		}
        
        if(m.getAddress() == "/2/toggle1" || m.getAddress() == "/1/toggle1"){
            animateShow = m.getArgAsInt32(0);
        }

        
        else{ sender.sendMessage(m);}
        
	}
    
    
    //3d model related
    model.update();
    if(bAnimateMouse) {
        model.setPositionForAllAnimations(animationPosition);
    }
    mesh = model.getCurrentAnimatedMesh(0);
}

//--------------------------------------------------------------
void ofApp::draw(){
    ofSetColor(255);
    
    ofEnableBlendMode(OF_BLENDMODE_ALPHA);
    
	ofEnableDepthTest();
    
    glShadeModel(GL_SMOOTH); //some model / light stuff
    light.disable();
    
    //ofSetColor(255,0,0);
    //ofDrawSphere(mouseX,mouseY,0,20);
    
    //lighting
    ofSetColor(255,255,255);
    light.enable();
    ofEnableSeparateSpecularLight();
    light.setPointLight();
    light.setAttenuation(bright);
    light.setPosition(ofVec3f(mouseX,mouseY,-500));
    

    //start shader stuff
        
    shader.begin();
    //we want to pass in some varrying values to animate our type / color
    shader.setUniform1f("height", twistHeight );
    shader.setUniform1f("bendAngle", bendAngle );
    shader.setUniform1f("twistAngle", twistAmount );
    if (animateShow) {
        shader.setUniform1f("twistAmount", twistAmount * sin(ofGetElapsedTimef()* twistFreq ) );
        shader.setUniform1f("bendAmount", bendAmount * (sin(ofGetElapsedTimef() * bendFreq) + .2 * sin(ofGetElapsedTimef() * bendFreq*5. )));
    } else {
        shader.setUniform1f("twistAmount", twistAmount  );
        shader.setUniform1f("bendAmount", bendAmount );
        
    }
    shader.setUniform1f("exposure", exposure );
    
    //we also pass in the mouse position
    //we have to transform the coords to what the shader is expecting which is 0,0 in the center and y axis flipped.
    shader.setUniform2f("mouse", mouseX - ofGetWidth()/2, ofGetHeight()/2-mouseY );
    
	
	
    //draw our model
    
    ofPushMatrix();
    ofTranslate(model.getPosition().x+100, model.getPosition().y, 0);
    ofRotateX(90);
    ofRotateZ(rotZ);
    ofTranslate(-model.getPosition().x, -model.getPosition().y, 0);
    
    model.setScale(scale,scale,scale);
    model.setPosition(modelX, modelY,modelZ);
    model.drawFaces();
    ofPopMatrix();
    

 
    
    shader.end();
	
    if(ofGetGLProgrammableRenderer()){
		glPushAttrib(GL_ALL_ATTRIB_BITS);
		glPushClientAttrib(GL_CLIENT_ALL_ATTRIB_BITS);
    }
    glEnable(GL_NORMALIZE);
    
    ofDisableDepthTest();
    light.disable();
    
    ofDisableLighting();
    
    ofDisableSeparateSpecularLight();
    
    ofSetColor(255, 255, 255 );
    
    if (guiShow) {gui.draw();}
    
    //ofDrawBitmapString("Statue: "+ofToString(modelCurrentName, 2), 10, 600);
    
    
    lttf.drawString(ofToString(modelCurrentFile, 2), textX, textY);
    
    /*
    ofDrawBitmapString("fps: "+ofToString(ofGetFrameRate(), 2), 10, 15);
    ofDrawBitmapString("keys 1-5 load models, spacebar to trigger animation", 10, 30);
    ofDrawBitmapString("drag to control animation with mouseY", 10, 45);
    ofDrawBitmapString("num animations for this model: " + ofToString(model.getAnimationCount()), 10, 60);
    ofDrawBitmapString("mouseX, mouseY: "+ofToString(mouseX)+" "+ofToString(mouseY), 10, 75);
     */
    
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){
    ofPoint modelPosition(ofGetWidth() * 0.5, (float)ofGetHeight() * 0.75);
    switch (key) {
        case OF_KEY_LEFT:
            modelCurrent -= 1;
            if (modelCurrent < 0) {modelCurrent = 0;}
            break;
        case OF_KEY_RIGHT:
            modelCurrent += 1;
            if (modelCurrent >= modelCount) {modelCurrent = modelCount - 1;}
            break;
        case OF_KEY_RETURN:
            modelPath =modelDir.getPath(modelCurrent)+"/sculpt.obj";
            model.loadModel(modelPath);
            model.setPosition(modelPosition.x, modelPosition.y, modelPosition.z);
            ofEnableSeparateSpecularLight();
           
            
            mesh = model.getMesh(0);
        case ' ':
			bAnimate = !bAnimate;
			break;
        default:
            break;
    }
    
    modelCurrentName =modelDir.getPath(modelCurrent);
    modelCurrentFile =modelCurrentName.erase(0,7);

    /* animation related
    model.setLoopStateForAllAnimations(OF_LOOP_NORMAL);
    model.playAllAnimations();
    if(!bAnimate) {
    model.setPausedForAllAnimations(true);
     */
    

}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){
    //
}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){
    // scrub through aninations manually.
	animationPosition = y / (float)ofGetHeight();
}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){
    // pause all animations, so we can scrub through them manually.
    model.setPausedForAllAnimations(true);
	animationPosition = y / (float)ofGetHeight();
    bAnimateMouse = true;
}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){
    // unpause animations when finished scrubbing.
    if(bAnimate) {
        model.setPausedForAllAnimations(false);
    }
    bAnimateMouse = false;
}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){

}



